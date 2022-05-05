using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    
    [SerializeReference]
    public Texture2D bend;
    
    [SerializeReference]
    public Texture2D corner;
    
    [SerializeReference]
    public Texture2D corridor;
    
    [SerializeReference]
    public Texture2D door;
    
    [SerializeReference]
    public Texture2D empty;
    
    [SerializeReference]
    public Texture2D poi;
    
    [SerializeReference]
    public Texture2D side;
    
    [SerializeReference]
    public Texture2D t;
    
    [SerializeReference]
    public Texture2D turn;
    
    [SerializeReference]
    public Texture2D wall;

    void Start()
    {

    }

    // Update is called once per frame
    internal Texture2D GenerateMap(int size) {

        var tileConfig = new List<XTile>();
        tileConfig.Add(new XTile(bend, "bend","L","0.5"));
        tileConfig.Add(new XTile(corner, "corner","L","0.5"));
        tileConfig.Add(new XTile(corridor, "corridor","I","1.0"));
        tileConfig.Add(new XTile(door, "door","T","0.5"));
        tileConfig.Add(new XTile(empty, "empty","X"));
        tileConfig.Add(new XTile(side, "side","T","2.0"));
        tileConfig.Add(new XTile(t, "t","T","0.5"));
        tileConfig.Add(new XTile(turn, "turn","L","0.25"));
        tileConfig.Add(new XTile(wall, "wall","X"));
        tileConfig.Add(new XTile(poi, "poi","X","0.1")); // change this weight for more or less spawns

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

        bool periodic = false; 
        bool blackBackground = false;
        SimpleTiledModel model = new SimpleTiledModel(size,size,periodic,blackBackground,Model.Heuristic.Entropy, tileConfig, neighConfig);
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