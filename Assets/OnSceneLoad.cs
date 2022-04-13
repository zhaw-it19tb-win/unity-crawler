using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OnSceneLoad : MonoBehaviour
{
    GameObject[] playerObjs;
    GameObject[] teleporterObjs;
    void Start()
    {
        playerObjs = GameObject.FindGameObjectsWithTag("Player");
        teleporterObjs = GameObject.FindGameObjectsWithTag("Teleporter");

        if (playerObjs.Length == 1)
         {
            playerObjs[0].transform.localPosition = new Vector3(1, 0, 0); // Bug : teleporterObjs[0].transform.localPosition; --> Position not correct
        }
        
    }
}
