using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ProceduralGenerate : MonoBehaviour
{
    public Tilemap layer0;

    public Tilemap layer1;

    //public Tilemap layerCollision;

    [SerializeReference]
    public GameObject teleporterPrefab;

    [SerializeField]
    public int startX = -50;
    [SerializeField]
    public int endX = 50;
    [SerializeField]
    public int startY = -50;
    [SerializeField]
    public int endY = 50;

    [SerializeField]
    public int spawnX = 5;
    [SerializeField]
    public int spawnY = 5;

    [SerializeReference]
    public MapData mapData;

    void Awake() {       
    }

    private void Start() {
        TryToLoadLevel();
        PopulateScene();
        AwakeScene();
    }

    private void TryToLoadLevel()
    {
        string savefile = "proc_dungeon_1.json";
        mapData = JsonUtility.FromJson<MapData>(SaveGameManager.LoadData(savefile));
        if (mapData != null) {
            SetDungeonData( mapData );
        } else {
            mapData = GenerateDungeonData( startX, endX, startY, endY, spawnX, spawnY );
            SaveGameManager.SaveData( JsonUtility.ToJson(mapData), savefile );
        }
    }

    private void PopulateScene()
    {
        // Place tiles

        // Get available tiles
        // tile index 0 = floor
        // tile index 1 = wall
        // tile index 2 = spawner
        // tile index 3 = collider
        MapTiles tiles = GameObject.FindGameObjectWithTag("MapTiles").GetComponentInChildren<MapTiles>();
        Tile[][] tilebases = new Tile[4][];
        tilebases[0] = tiles.floorTiles;
        tilebases[1] = tiles.wallTiles;
        tilebases[2] = tiles.spawnTiles;
        tilebases[3] = tiles.collisionTiles;

        // Get references to tilemap layers
        layer0 = GetComponentsInChildren<Tilemap>()[0];
        layer1 = GetComponentsInChildren<Tilemap>()[1];
        //layerCollision = GameObject.FindGameObjectWithTag("CollisionLayer").GetComponent<Tilemap>();


        foreach (DungeonTile t in mapData.layer0List) {
            layer0.SetTile(new Vector3Int(t.x, t.y, t.z), tilebases[t.cat][t.idx]);
        }
        foreach (DungeonTile t in mapData.layer1List) {
            layer1.SetTile(new Vector3Int(t.x, t.y, t.z), tilebases[t.cat][t.idx]);
        }
        //foreach (DungeonTile t in mapData.layerCollisionList) {
        //    layerCollision.SetTile(new Vector3Int(t.x, t.y, t.z), tilebases[t.cat][t.idx]);
        //}

        // Place spawn Teleporter Collider
        Vector3 centreOfTelporter = MapUtil.getCentreOfTile(spawnX, spawnY);
        GameObject spawnObj = Instantiate(teleporterPrefab, centreOfTelporter + new Vector3(0,0,-1), Quaternion.identity );
        Teleporter spawnTeleporter = spawnObj.GetComponent<Teleporter>();
        spawnTeleporter.teleporterId = "ProcDungeon_1_0";
        spawnTeleporter.targetScene = "MainScene";
        spawnTeleporter.targetTeleporterId = "MainScene_1";

    }

    private IEnumerator AwakeScene()
    {
        // Refresh composite collision collider
        yield return new WaitForEndOfFrame(); // need this!
        //layerCollision.GetComponent<CompositeCollider2D>().GenerateGeometry();
        layer1.GetComponent<CompositeCollider2D>().GenerateGeometry();
        yield return new WaitForEndOfFrame();
    }

    // Generate the dungeon procedurally...
    private MapData GenerateDungeonData( int startX, int endX, int startY, int endY, int spawnX, int spawnY ) {

        MapData result = new MapData();

        result.startX = startX;
        result.endX = endX;
        result.startY = startY;
        result.endY = endY;
        result.spawnX = spawnX;
        result.spawnY = spawnY;

        result.layer0List = new List<DungeonTile>();
        result.layer1List = new List<DungeonTile>();
        //result.layerCollisionList = new List<DungeonTile>();

        result.teleporterPrefab = teleporterPrefab;
        
        // Get available tiles
        MapTiles tiles = GameObject.FindGameObjectWithTag("MapTiles").GetComponentInChildren<MapTiles>();
        
        // Go through all the tiles
        for (int i = startX; i <= endX; i++) {
            for (int j = startY; j <= endY; j++) {
                // tile index 0 = floor
                // tile index 1 = wall
                // tile index 2 = spawner
                // tile index 3 = collider
                if (i == startX || i == endX || j == startY || j == endY) {
                    // Set wall tiles if at edge:
                    result.layer0List.Add( new DungeonTile( i, j, 0, 1, UnityEngine.Random.Range(0, tiles.wallTiles.Length) ) );
                    result.layer1List.Add( new DungeonTile( i, j, 2, 1, UnityEngine.Random.Range(0, tiles.wallTiles.Length) ) );
                    // using collision layer not needed, since composite collider is on layer1 for dungeons
                    // result.layerCollisionList.Add( new DungeonTile( i, j, 0, 3, 0 ) );
                } else if ( i == spawnX && j == spawnY ) {
                    // Set spawn tile if at spawn
                    result.layer0List.Add( new DungeonTile( i, j, 0, 2, 0) );
                } else {
                    // Set floor tile for all other tiles:
                    result.layer0List.Add( new DungeonTile( i, j, 0, 0, UnityEngine.Random.Range(0, tiles.floorTiles.Length) ) );
                    // additionally add 2ndlevel of collision
                    // ...actually not needed for dungeons since collider is on layer1 
                    //if (i == startX+1 || j == startY+1)  {
                    //    // Set 2nd collider level tiles
                    //    result.layerCollisionList.Add( new DungeonTile( i, j, 0, 3, 0) ); 
                    //}
                }
            }
        }
       
        return result;        
    }

    public void SetDungeonData(MapData data) {
        this.teleporterPrefab = data.teleporterPrefab;
        this.startX = data.startX;
        this.endX = data.endX;
        this.startY = data.startY;
        this.endY = data.endY;
        this.spawnX = data.spawnX;
        this.spawnY = data.spawnY;
    }
}

[Serializable]
public class DungeonTile {

    public DungeonTile(int x, int y, int z, int tileCategory, int tileIndex){
        this.x = x;
        this.y = y;
        this.z = z;
        this.cat = tileCategory;
        this.idx = tileIndex;
    }

    public int x;
    public int y;
    public int z;
    public int cat;
    public int idx;
}

[Serializable]
public class MapData {

    public List<DungeonTile> layer0List;
    public List<DungeonTile> layer1List;
    //public List<DungeonTile> layerCollisionList;
    public GameObject teleporterPrefab;
    public int startX;
    public int endX;
    public int startY;
    public int endY;
    public int spawnX;
    public int spawnY;

}
