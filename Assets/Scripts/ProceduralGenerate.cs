using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.AI;

[RequireComponent(typeof(LevelGenerator))]
public class ProceduralGenerate : MonoBehaviour
{
    public static string MAPVERSION = "v0.1.3";

    [SerializeReference]
    public int desiredMapWidth = 15;

    [SerializeReference]
    public int desiredMapHeight = 15;
    
    public Tilemap layer0;

    public Tilemap layer1;

    [SerializeReference]
    public GameObject teleporterPrefab;

    public MapData mapData;

    public string mapDataId;

    void Awake() {       
    }

    private void Start() {
        LoadLevel();
        PopulateScene();
        AwakeScene();
    }

    private string GetMapDataId() {
        //return GameUtil.GameId + UnityEngine.SceneManagement.SceneManager.GetActiveScene().name + MAPVERSION;
        return GameUtil.GameId + UnityEngine.SceneManagement.SceneManager.GetActiveScene().name + MAPVERSION + "_"+System.DateTime.UtcNow.ToString();
    }

    private void LoadLevel()
    {
        string savefile = "savefile1.json";
        mapData = JsonUtility.FromJson<MapData>(SaveGameManager.LoadData(savefile));
        if (mapData == null || mapData.mapDataId != GetMapDataId()) {
            //mapData = DungeonDataFromImage(GenerateMap(desiredMapWidth, desiredMapHeight));
            mapData = DungeonDataFromImage(PreProcessImage(GenerateMap(desiredMapWidth, desiredMapHeight)));
            SaveGameManager.SaveData( JsonUtility.ToJson(mapData), savefile );
        } 
    }

    
    private void PopulateScene()
    {
        // Place tiles...
        // Available tile categories so far...
        // tile index 0 = floor
        // tile index 1 = wall
        // tile index 2 = spawner

        MapTiles tiles = GameObject.FindGameObjectWithTag("MapTiles").GetComponentInChildren<MapTiles>();
        UnityEngine.Tilemaps.Tile[][] tilebases = new UnityEngine.Tilemaps.Tile[3][];
        tilebases[0] = tiles.floorTiles;
        tilebases[1] = tiles.wallTiles;
        tilebases[2] = tiles.spawnTiles;

        // Get references to tilemap layers
        layer0 = GetComponentsInChildren<Tilemap>()[0];
        layer1 = GetComponentsInChildren<Tilemap>()[1];

        // Place tiles from mapData
        foreach (DungeonTile t in mapData.layer0List) {
            layer0.SetTile(new Vector3Int(t.x, t.y, t.z), tilebases[t.cat][t.idx]);
        }
        foreach (DungeonTile t in mapData.layer1List) {
            layer1.SetTile(new Vector3Int(t.x, t.y, t.z), tilebases[t.cat][t.idx]);
        }

        // Place spawn Teleporter Collider
        Vector3 centreOfSpawnTelporter = MapUtil.getCentreOfTile(mapData.spawnX, mapData.spawnY);
        GameObject spawnObj = Instantiate(teleporterPrefab, centreOfSpawnTelporter + new Vector3(0,0,-1), Quaternion.identity );
        Teleporter spawnTeleporter = spawnObj.GetComponent<Teleporter>();
        spawnTeleporter.Location = CardinalDirection.North;

        // Place exit Teleporter Collider
        Vector3 centreOfExitTelporter = MapUtil.getCentreOfTile(mapData.exitX, mapData.exitY);
        GameObject exitObj = Instantiate(teleporterPrefab, centreOfExitTelporter + new Vector3(0,0,-1), Quaternion.identity );
        Teleporter exitTeleporter = spawnObj.GetComponent<Teleporter>();
        exitTeleporter.Location = CardinalDirection.South;
    }

    private IEnumerator AwakeScene()
    {
        // Refresh composite collision collider
        yield return new WaitForEndOfFrame(); // need this!
        layer1.GetComponent<CompositeCollider2D>().GenerateGeometry();
        yield return new WaitForEndOfFrame();
    }

    private MapData DungeonDataFromImage(Texture2D mapFile){

        MapData result = new MapData();

        result.mapDataId = GetMapDataId();
        result.startX = 0;
        result.endX = mapFile.width;
        result.startY = 0;
        result.endY = mapFile.height;
        result.layer0List = new List<DungeonTile>();
        result.layer1List = new List<DungeonTile>();
        
        MapTiles tiles = GameObject.FindGameObjectWithTag("MapTiles").GetComponentInChildren<MapTiles>();
        
        for (int i = result.startX; i < result.endX; i++) {
            for (int j = result.startY; j < result.endY; j++) {
                Color pixel = mapFile.GetPixel(i,j);
                if (pixel.Equals(Color.black)) {
                    // draw wall on layer 0 and layer 1
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
                    // TODO: place enemy at i,j..
                    result.layer0List.Add( new DungeonTile( i, j, 0, 2, 2) );
                    // TODO: use some different tile..
                } else {
                    Debug.Log("No tile to add for pixel["+i+"]["+j+"]: " + pixel);
                }
            }
        }
        return result;
    }

    private Texture2D PreProcessImage(Texture2D input) {
        // For a generated 20x20 tilemap with 3x3 tiles, we get a 60x60 input image...

        // We track a list of red pois to turn into enter, exit and enemy spawners
        var poiList = new List<(Vector2Int,Color)>();
        // We will add a wall border all around the generated map: 60 x 60 -> 62 x 62 
        // Finally we will double the image to ensure we can walk through all the paths ( 62 x 62 -> 124 x 124)
        Texture2D result = new Texture2D((input.width + 2) * 2, (input.height + 2) * 2);
        result.filterMode = FilterMode.Point; // This is necessary to get accurate pixel colors, not interpolated...

        // GENERATE SIDE WALLS
        // k will loop through 0 -> 61
        for (int k = 0; k < input.height + 2; k++) {
            result.SetPixel( 0, 2*k, Color.black); // set left bottom wall
            result.SetPixel( 0, 2*k + 1, Color.black); // set left bottom wall
            result.SetPixel( 1, 2*k, Color.black); // set left bottom wall
            result.SetPixel( 1, 2*k + 1, Color.black); // set left bottom wall

            result.SetPixel( 2*input.width + 2, 2*k, Color.black); // set right top as wall
            result.SetPixel( 2*input.width + 2, 2*k + 1, Color.black); // set right top as wall
            result.SetPixel( 2*input.width + 3, 2*k, Color.black); // set right top as wall
            result.SetPixel( 2*input.width + 3, 2*k + 1, Color.black); // set right top as wall
        }

        // k will loop through 0 -> 61
        for (int k = 0; k < input.width + 2; k++) {
            result.SetPixel( 2*k, 0, Color.black); // set right bottom as wall
            result.SetPixel( 2*k + 1, 0, Color.black); // set right bottom  as wall
            result.SetPixel( 2*k, 1, Color.black); // set right bottom as wall
            result.SetPixel( 2*k + 1, 1, Color.black); // set right bottom as wall

            result.SetPixel( 2*k, 2*input.height + 2, Color.black); // set top left as wall
            result.SetPixel( 2*k + 1, 2*input.height + 2, Color.black); // set top left as wall
            result.SetPixel( 2*k, 2*input.height + 3, Color.black); // set top left as wall
            result.SetPixel( 2*k + 1, 2*input.height + 3, Color.black); // set top left as wall
        }

        // PLACE PIXELS FROM MODEL
        for (int i = 1; i < input.width + 1; i++) {
            for (int j = 1; j < input.height + 1; j++) {
                Color currentPixel = input.GetPixel(i,j);
                if(Color.red.Equals(currentPixel)) {
                    poiList.Add( (new Vector2Int(2*i + 1, 2*j + 1), currentPixel) );
                    // red pixels should not get scaled, we only want i.e. 1 spawner, not 4..
                    result.SetPixel(2*i, 2*j, Color.white );
                    result.SetPixel(2*i, (2*j) + 1, Color.white );
                    result.SetPixel((2*i) + 1, 2*j, Color.white );
                    result.SetPixel((2*i) + 1, (2*j) + 1, currentPixel );
                } else {
                    result.SetPixel(2*i, 2*j,  currentPixel );
                    result.SetPixel(2*i, (2*j) + 1, currentPixel );
                    result.SetPixel((2*i) + 1, 2*j, currentPixel );
                    result.SetPixel((2*i) + 1, (2*j) + 1, currentPixel);
                }
            }
        }

        // try 3 times to find start and exits points that are far apart
        int tries = 3;
        Vector2Int bestStart = poiList.ElementAt(UnityEngine.Random.Range(0, poiList.Count)).Item1;
        Vector2Int bestExit = poiList.ElementAt(UnityEngine.Random.Range(0, poiList.Count)).Item1;
        float bestDistance = Vector2.Distance(bestExit, bestStart);

        for (int i = 0; i < tries; i++) {
            Vector2Int currStart = poiList.ElementAt(UnityEngine.Random.Range(0, poiList.Count)).Item1;
            Vector2Int currExit = poiList.ElementAt(UnityEngine.Random.Range(0, poiList.Count)).Item1;
            float distance = Vector2.Distance(currExit, currStart);
            if (distance > bestDistance) {
                bestStart = currStart;
                bestExit = currExit;
                bestDistance = distance;
            }
        }

        // set spawners
        if (bestDistance == 0.0f) Debug.Log("Warning, distance between spawns is zero..");

        result.SetPixel( bestStart.x, bestStart.y, Color.green);
        result.SetPixel( bestExit.x, bestExit.y, Color.blue);
        
        return result;
    }

    private Texture2D GenerateMap(int desiredMapWidth, int desiredMapHeight)
    {
        LevelGenerator gen = GameObject.FindObjectOfType<LevelGenerator>();
        Texture2D result = null; 
        bool goodDungeon = false;
        while (!goodDungeon) {
            
            result = gen.GenerateMap(desiredMapWidth,desiredMapHeight);
            result.filterMode = FilterMode.Point;

            List<List<Vector2Int>> found = new List<List<Vector2Int>>();
            
            List<Vector2Int> neighbors = new List<Vector2Int>(){Vector2Int.up, Vector2Int.left, Vector2Int.down, Vector2Int.right};

            for (int i = 0; i < result.width; i++) for (int j = 0; j < result.height; j++) {
                if( found.SelectMany(x => x).ToList().Contains(new Vector2Int(i,j)) ) continue; // contine to next pixel - we already found this one 
                if( Color.black.Equals(result.GetPixel(i,j)) ) continue; // continue to next pixel - this is a wall 
                List<Vector2Int> todo = new List<Vector2Int>(); // found walkable pixel - we create a stack for connected pixels
                List<Vector2Int> currArea = new List<Vector2Int>(); // we reserve a list for the pixels we discover from this start

                todo.Add(new Vector2Int(i, j)); // add our point to the stack and iterate

                while (todo.Count > 0) {
                    Vector2Int curr = todo.ElementAt(0);
                    foreach (Vector2Int dir in neighbors) {
                        Vector2Int neighbor = curr + dir;
                        Color neighCol = result.GetPixel(neighbor.x, neighbor.y);
                        if (neighbor.x < 0 || neighbor.y < 0 || neighbor.x >= result.width || neighbor.y >= result.height || // check if out of bounds
                            neighCol.Equals(Color.black) || // check if we hit a wall
                            todo.Contains(neighbor) || currArea.Contains(neighbor)) { // check if we already added this to stack or current area
                                continue; // if any of the above -> disregard this neighbor
                        } else {
                            todo.Add(neighbor); // good neighbor, add to stack
                        }
                    }
                    currArea.Add(curr); // added all neighbors, now can add this tile to the current area
                    todo.Remove(curr); // remove this tile from the todo stack and check if anything is left in todo
                }
                found.Add(currArea); // stack cleared.. add area to found areas
            }

            found.Sort (delegate(List<Vector2Int> a, List<Vector2Int> b) { return b.Count.CompareTo(a.Count); });
            Debug.Log("Number of areas: " + found.Count.ToString());

            // Check largest area if it is too small:
            Debug.Log("map height:" + result.height);
            Debug.Log("map width:" + result.width);
            Debug.Log("map area: " + result.height * result.width);

            float walkableArea = (float)found.ElementAt(0).Count / (float)( result.height * result.width );
            if ( walkableArea <= 0.5f ) {
                Debug.Log("Walkable area is less than 50%");
                continue; // restart loop...
            } else {
                Debug.Log("Good map. Walkable area is: " + walkableArea);
                goodDungeon = true;
            }

            for( int i = 0; i < found.Count; i++) { // only show non-main areas (index 1 and above, index 0 has largest area)
                var area = found[i];
                Debug.Log("Connected Tiles: " + area.Count.ToString());
                Debug.Log("Coordinates: " + area[0] + " .. " + area.Last());    
            } 
            
            for (int i = 1; found.Count > i; i++) {
                found.ElementAt(i).ForEach(dot => result.SetPixel(dot.x, dot.y, Color.black)); // overwriting all not connected dungeons
            }

            // TODO check 
        }
        return result;
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

    public string mapDataId;

    public List<DungeonTile> layer0List;
    public List<DungeonTile> layer1List;

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
