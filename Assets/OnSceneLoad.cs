using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnSceneLoad : MonoBehaviour
{
    void Start()
    {
        GameObject[] playerObjs = GameObject.FindGameObjectsWithTag("Player");
        GameObject[] teleporterObjs = GameObject.FindGameObjectsWithTag("Teleporter");

        if (playerObjs.Length == 1)
        {
             playerObjs[0].transform.position = teleporterObjs[0].transform.position;
        }
        
    }
}
