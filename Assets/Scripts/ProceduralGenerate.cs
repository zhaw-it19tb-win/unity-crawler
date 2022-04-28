using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ProceduralGenerate : MonoBehaviour
{
    public static string MAPVERSION = "v0.1.0";
    
    public Tilemap layer0;

    public Tilemap layer1;

    //public Tilemap layerCollision;

    [SerializeReference]
    public GameObject teleporterPrefab;

    //[SerializeField]
    //public int startX = -50;
    //[SerializeField]
    //public int endX = 50;
    //[SerializeField]
    //public int startY = -50;
    //[SerializeField]
    //public int endY = 50;
    //[SerializeField]
    //public int spawnX = 5;
    //[SerializeField]
    //public int spawnY = 5;
    //[SerializeReference]
    public MapData mapData;

    void Awake() {       
    }

    private void Start() {
        LoadLevel();
        PopulateScene();
        AwakeScene();
    }

    private void LoadLevel()
    {
        string savefile = GameUtil.GameId + UnityEngine.SceneManagement.SceneManager.GetActiveScene().name + MAPVERSION + ".json";
        //Debug.Log("savefile="+savefile);
        mapData = JsonUtility.FromJson<MapData>(SaveGameManager.LoadData(savefile));
        if (mapData == null) {
            //mapData = GenerateDungeonData( 0, 20, 0, 20, 10, 10 );
            //mapData = DungeonDataFromImage(ImageFromFile("demo_dungeon"));
            //mapData = DungeonDataFromImage(ImageFromFile("debug_dungeon"));
            mapData = DungeonDataFromGeneratedImage(ImageFromFile("rand1"));
            //this.teleporterPrefab = mapData.teleporterPrefab;
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
        Vector3 centreOfSpawnTelporter = MapUtil.getCentreOfTile(mapData.spawnX, mapData.spawnY);
        GameObject spawnObj = Instantiate(teleporterPrefab, centreOfSpawnTelporter + new Vector3(0,0,-1), Quaternion.identity );
        Teleporter spawnTeleporter = spawnObj.GetComponent<Teleporter>();
        spawnTeleporter.teleporterId = "ProcDungeon_1_0";
        spawnTeleporter.targetScene = "MainScene";
        spawnTeleporter.targetTeleporterId = "MainScene_1";

        // Place spawn Teleporter Collider
        Vector3 centreOfExitTelporter = MapUtil.getCentreOfTile(mapData.exitX, mapData.exitY);
        GameObject exitObj = Instantiate(teleporterPrefab, centreOfExitTelporter + new Vector3(0,0,-1), Quaternion.identity );
        Teleporter exitTeleporter = spawnObj.GetComponent<Teleporter>();
        exitTeleporter.teleporterId = "ProcDungeon_1_1";
        exitTeleporter.targetScene = "MainScene";
        exitTeleporter.targetTeleporterId = "MainScene_1";

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

        //result.teleporterPrefab = teleporterPrefab;
        
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

    private MapData DungeonDataFromImage(Texture2D mapFile){
        //Texture2D mapFile = Resources.Load(filename) as Texture2D;

        Debug.Log(mapFile);

        MapData result = new MapData();

        result.startX = 0;
        result.endX = mapFile.width;
        result.startY = 0;
        result.endY = mapFile.height;

        result.layer0List = new List<DungeonTile>();
        result.layer1List = new List<DungeonTile>();
        //result.teleporterPrefab = teleporterPrefab;
        
        // Get available tiles
        MapTiles tiles = GameObject.FindGameObjectWithTag("MapTiles").GetComponentInChildren<MapTiles>();
        
        for (int i = result.startX; i < result.endX; i++) {
            for (int j = result.startY; j < result.endY; j++) {
                Color pixel = mapFile.GetPixel(i,j);
                if (i == result.startX || i == result.endX-1 || j == result.startY || j == result.endY-1) {
                    // Set wall tiles if at edge:
                    result.layer0List.Add( new DungeonTile( i, j, 0, 1, UnityEngine.Random.Range(0, tiles.wallTiles.Length) ) );
                    result.layer1List.Add( new DungeonTile( i, j, 2, 1, UnityEngine.Random.Range(0, tiles.wallTiles.Length) ) );
                }
                else if (pixel.Equals(Color.black)) {
                    // draw wall
                    result.layer0List.Add( new DungeonTile( i, j, 0, 1, UnityEngine.Random.Range(0, tiles.wallTiles.Length) ) );
                    result.layer1List.Add( new DungeonTile( i, j, 2, 1, UnityEngine.Random.Range(0, tiles.wallTiles.Length) ) );
                } else if (pixel.Equals(Color.white)) {
                    // draw only floor
                    result.layer0List.Add( new DungeonTile( i, j, 0, 0, UnityEngine.Random.Range(0, tiles.floorTiles.Length) ) );
                } else if (pixel.Equals(Color.green)) {
                    // friendly spawner
                    result.spawnX = i;
                    result.spawnY = j;
                    result.layer0List.Add( new DungeonTile( i, j, 0, 2, 0) );
                } else if (pixel.Equals(Color.blue)) {
                    // friendly exit
                    result.exitX = i;
                    result.exitY = j;
                    result.layer0List.Add( new DungeonTile( i, j, 0, 2, 1) );
                } else if (pixel.Equals(Color.red)) {
                    // TODO: place enemy spawner at i,j..
                    Debug.Log("TODO: place enemy spawner here..");
                } else {
                    //result.layer0List.Add( new DungeonTile( i, j, 0, 1, UnityEngine.Random.Range(0, tiles.wallTiles.Length) ) );
                    //result.layer1List.Add( new DungeonTile( i, j, 2, 1, UnityEngine.Random.Range(0, tiles.wallTiles.Length) ) );
                    Debug.Log("Pixel["+i+"]["+j+"] not found: " + pixel);
                }
            }
        }

        return result;
    }
    private MapData DungeonDataFromGeneratedImage(Texture2D input)
    {
        MapData result = DungeonDataFromImage(DoubleImage(input));
        
        DungeonTile[] filtered = result.layer1List.FindAll(e => e.cat==0).ToArray();
        int validSpawnX = UnityEngine.Random.Range(0, result.endX-result.startX);
        int validSpawnY = UnityEngine.Random.Range(0, result.endY-result.startY);
        while(result.layer0List.Find((e) => e.x == validSpawnX && e.y == validSpawnY).cat != 0) {
            validSpawnX = UnityEngine.Random.Range(0, result.endX-result.startX);
            validSpawnY = UnityEngine.Random.Range(0, result.endY-result.startY);
        }

        int validExitX = UnityEngine.Random.Range(0, result.endX-result.startX);
        int validExitY = UnityEngine.Random.Range(0, result.endY-result.startY);
        while(result.layer0List.Find(e => e.x == validExitX && e.y == validExitY).cat != 0) {
            validExitX = UnityEngine.Random.Range(0, result.endX-result.startX);
            validExitY = UnityEngine.Random.Range(0, result.endY-result.startY);
        }

        result.spawnX = validSpawnX;
        result.spawnY = validSpawnY;
        result.layer0List.Add( new DungeonTile(  result.spawnX,  result.spawnY, 0, 2, 0) );

        result.exitX = validExitX;
        result.exitY = validExitY;
        result.layer0List.Add( new DungeonTile( result.exitX, result.exitY, 0, 2, 1) );
        
        return result;
    }
    

    private Texture2D DoubleImage(Texture2D input) {
        Texture2D result = new Texture2D(input.width*2, input.height*2);
        for (int i = 0; i < input.width; i++) {
            for (int j = 0; j < input.height; j++) {
                result.SetPixel(2*i, 2*j, input.GetPixel(i,j) );
                result.SetPixel(2*i, (2*j) + 1, input.GetPixel(i,j) );
                result.SetPixel((2*i) + 1, 2*j, input.GetPixel(i,j) );
                result.SetPixel((2*i) + 1, (2*j) + 1, input.GetPixel(i,j) );
            }
        }
        return result;
    }

    private Texture2D ImageFromFile(string filename) {
        return (Texture2D)Resources.Load(filename);
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
    //public GameObject teleporterPrefab;
    public int startX;
    public int endX;
    public int startY;
    public int endY;
    public int spawnX;
    public int spawnY;
    public int exitX;
    public int exitY;
    public int[] enemyX;
    public int[] enemyY;

}
