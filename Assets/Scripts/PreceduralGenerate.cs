using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PreceduralGenerate : MonoBehaviour
{
    private Tilemap layer0;

    private Tilemap layer1;

    private Tilemap layerCollision;


    [SerializeReference]
    private GameObject teleporterPrefab;

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

    void Awake() {       
    }

    private IEnumerator Start() {
        // Generate the dungeon procedurally...

        // Get references to tilemap layers
        layer0 = GetComponentsInChildren<Tilemap>()[0];
        layer1 = GetComponentsInChildren<Tilemap>()[1];
        layerCollision = GameObject.FindGameObjectWithTag("CollisionLayer").GetComponent<Tilemap>();
        
        // Get available tiles
        MapTiles tiles = GameObject.FindGameObjectWithTag("MapTiles").GetComponentInChildren<MapTiles>();
        
        // Go through all the tiles
        for (int i = startX; i < endX; i++) {
            for (int j = startY; j < endY; j++) {
                Vector3Int currentCell = new Vector3Int(i, j, 0);

                // Set wall tiles if at edge:
                if (i == startX || i == endX-1 || j == startY || j == endY-1) {
                    layer0.SetTile( currentCell, tiles.wallTiles[Random.Range(0, tiles.wallTiles.Length)] );
                    layer1.SetTile( new Vector3Int(i, j, 2), tiles.wallTiles[Random.Range(0, tiles.wallTiles.Length)] );
                    layerCollision.SetTile( currentCell, tiles.collisionTiles[0] );
                } else {
                // Set floor tile else:
                    layer0.SetTile( currentCell, tiles.floorTiles[Random.Range(0, tiles.floorTiles.Length)] );
                }
            }
        }

        // Place spawn Teleporter Collider
        Vector3 centreOfTelporter = MapUtil.getCentreOfTile(spawnX, spawnY);
        GameObject spawnObj = Instantiate(teleporterPrefab, centreOfTelporter + new Vector3(0,0,-1), Quaternion.identity );
        Teleporter spawnTeleporter = spawnObj.GetComponent<Teleporter>();
        spawnTeleporter.teleporterId = "ProcDungeon_1_0";
        spawnTeleporter.targetScene = "MainScene";
        spawnTeleporter.targetTeleporterId = "MainScene_1";

        // Place spawn tiles
        layer0.SetTile( new Vector3Int(spawnX, spawnY, 0), tiles.spawnTiles[0]);

        // Refresh composite collision collider
        yield return new WaitForEndOfFrame(); // need this!
        layerCollision.GetComponent<CompositeCollider2D>().GenerateGeometry();
        yield return new WaitForEndOfFrame();
    }

}
