using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerPOI : MonoBehaviour
{
    public Vector3 pos;
    public int num;
    public int idx;

    // Start is called before the first frame update
    void Start()
    {
        if (pos != null) gameObject.transform.position = pos;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void Activate()
    {
        MapEnemies enemies = GameObject.FindObjectOfType<MapEnemies>();
        for (int i = 0; i < num; i++) {
            Instantiate(enemies.simpleEnemies[idx], pos, Quaternion.identity);
        }
    }
}
