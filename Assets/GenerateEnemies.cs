using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GenerateEnemies : MonoBehaviour
{
    public int number = 5;
    public SpawnPoint[] points;
    public GameObject archer;
    public GameObject archerBoss;
    public GameObject[] clones;

    void Awake()
    {
      Spawn();
    }

    void Spawn(){
      //int created = 0;
      clones = new GameObject[number];
      //while (created < number)
      for(int i = 0; i < number; i++)
      {
        SpawnPoint point = points[Random.Range(0,points.Length - 1)];
        GameObject clone = (GameObject)Instantiate(archer,
        new Vector3(point.x, point.y, point.z), Quaternion.identity);
        clones[i] = clone;
      }
    }



}
