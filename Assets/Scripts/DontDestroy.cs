using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    void Awake()
    {
        GameObject[] playObjs = GameObject.FindGameObjectsWithTag("Player");
        GameObject[] healthObjs = GameObject.FindGameObjectsWithTag("Healthbar");

        if (playObjs.Length > 1 || healthObjs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
}
