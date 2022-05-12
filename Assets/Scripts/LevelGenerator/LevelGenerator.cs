using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{   
    [SerializeReference]
    public int tilesize = 3;

    [SerializeReference]
    public Texture2D bend;

    [SerializeReference]
    public float bendWeight = 0.5f;
    
    [SerializeReference]
    public Texture2D corner;

    [SerializeReference]
    public float cornerWeight = 0.5f;
    
    [SerializeReference]
    public Texture2D corridor;

    [SerializeReference]
    public float corridorWeight = 1.0f;
    
    [SerializeReference]
    public Texture2D door;

    [SerializeReference]
    public float doorWeight = 0.5f;
    
    [SerializeReference]
    public Texture2D empty;

    [SerializeReference]
    public float emptyWeight = 1.0f;
    
    [SerializeReference]
    public Texture2D poi;

    // change this weight for more or less spawns
    [SerializeReference]
    public float poiWeight = 0.7f;
    
    [SerializeReference]
    public Texture2D side;

    [SerializeReference]
    public float sideWeight = 2.0f;
    
    [SerializeReference]
    public Texture2D t;

    [SerializeReference]
    public float tWeight = 0.5f;
    
    [SerializeReference]
    public Texture2D turn;

    [SerializeReference]
    public float turnWeight = 0.25f;
    
    [SerializeReference]
    public Texture2D wall;

    [SerializeReference]
    public float wallWeight = 1.0f;

    void Start()
    {

    }

    internal Texture2D GenerateMap(int width, int height) {

        var tileConfig = new List<XTile>();
        tileConfig.Add(new XTile(bend, "bend","L",bendWeight.ToString()));
        tileConfig.Add(new XTile(corner, "corner","L",cornerWeight.ToString()));
        tileConfig.Add(new XTile(corridor, "corridor","I",corridorWeight.ToString()));
        tileConfig.Add(new XTile(door, "door","T",doorWeight.ToString()));
        tileConfig.Add(new XTile(empty, "empty","X",emptyWeight.ToString()));
        tileConfig.Add(new XTile(side, "side","T",sideWeight.ToString()));
        tileConfig.Add(new XTile(t, "t","T",tWeight.ToString()));
        tileConfig.Add(new XTile(turn, "turn","L",turnWeight.ToString()));
        tileConfig.Add(new XTile(wall, "wall","X",wallWeight.ToString()));
        tileConfig.Add(new XTile(poi, "poi","X",poiWeight.ToString()));

        var neighConfig = new List<Neighbor>();
        neighConfig.Add(new Neighbor("corner 1","corner"));
        neighConfig.Add(new Neighbor("corner 2","corner"));
        neighConfig.Add(new Neighbor("corner","door"));
        neighConfig.Add(new Neighbor("corner","side 2"));
        neighConfig.Add(new Neighbor("corner 1","side 1"));
        neighConfig.Add(new Neighbor("corner 1","t 1"));
        neighConfig.Add(new Neighbor("corner 1","turn"));
        neighConfig.Add(new Neighbor("corner 2","turn"));
        neighConfig.Add(new Neighbor("wall","corner"));
        neighConfig.Add(new Neighbor("corridor 1","corridor 1"));
        neighConfig.Add(new Neighbor("corridor 1","door 3"));
        neighConfig.Add(new Neighbor("corridor","side 1"));
        neighConfig.Add(new Neighbor("corridor 1","t"));
        neighConfig.Add(new Neighbor("corridor 1","t 3"));
        neighConfig.Add(new Neighbor("corridor 1","turn 1"));
        neighConfig.Add(new Neighbor("corridor","wall"));
        neighConfig.Add(new Neighbor("door 1","door 3"));
        neighConfig.Add(new Neighbor("door 3","empty"));
        neighConfig.Add(new Neighbor("door","side 2"));
        neighConfig.Add(new Neighbor("door 1","t"));
        neighConfig.Add(new Neighbor("door 1","t 3"));
        neighConfig.Add(new Neighbor("door 1","turn 1"));
        neighConfig.Add(new Neighbor("empty","empty"));
        neighConfig.Add(new Neighbor("empty","side 3"));
        neighConfig.Add(new Neighbor("side","side"));
        neighConfig.Add(new Neighbor("side 3","side 1"));
        neighConfig.Add(new Neighbor("side 3","t 1"));
        neighConfig.Add(new Neighbor("side 3","turn"));
        neighConfig.Add(new Neighbor("side 3","wall"));
        neighConfig.Add(new Neighbor("t","t 2"));
        neighConfig.Add(new Neighbor("t","turn 1"));
        neighConfig.Add(new Neighbor("t 3","wall"));
        neighConfig.Add(new Neighbor("turn","turn 2"));
        neighConfig.Add(new Neighbor("turn 1","wall"));
        neighConfig.Add(new Neighbor("wall","wall"));
        neighConfig.Add(new Neighbor("bend","bend 1"));
        neighConfig.Add(new Neighbor("corner","bend 2"));
        neighConfig.Add(new Neighbor("door","bend 2"));
        neighConfig.Add(new Neighbor("empty","bend"));
        neighConfig.Add(new Neighbor("side","bend 1"));
        neighConfig.Add(new Neighbor("door 3","poi"));
        neighConfig.Add(new Neighbor("poi","poi"));
        neighConfig.Add(new Neighbor("poi","empty"));
        neighConfig.Add(new Neighbor("empty","poi"));
        neighConfig.Add(new Neighbor("poi","side 3"));
        neighConfig.Add(new Neighbor("poi","bend"));

        SimpleTiledModel model = new SimpleTiledModel(width, height, tileConfig, neighConfig);
        System.Random rand = new System.Random();
        bool success = model.Run(rand.Next(), -1);
        
        return model.Graphics();
    }
}

internal class Neighbor
{
    public string left;
    public string right;
    public Neighbor(string left, string right) {
        this.left = left;
        this.right = right;
    }
}

internal class XTile
{
    public string name;

    public string symmetry;
    public string weight;
    public Texture2D texture;

    public XTile(Texture2D texture, string name, string symmetry)
    {
        this.texture = texture;
        this.name = name;
        this.symmetry = symmetry;
    }

    public XTile(Texture2D texture, string name, string symmetry, string weight)
    {
        this.texture = texture;
        this.name = name;
        this.symmetry = symmetry;
        this.weight = weight;
    }
}