using UnityEngine;
using UnityEngine.Tilemaps;

public class MapTiles : MonoBehaviour
{
    [SerializeReference]
    public Tile[] floorTiles;
    
    [SerializeReference]
    public Tile[] wallTiles;
    
    [SerializeReference]
    public Tile[] spawnTiles;

    [SerializeReference]
    public Tile[] collisionTiles;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
