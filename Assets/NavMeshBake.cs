using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshBake : MonoBehaviour
{
    public NavMeshSurface Surface2D;

    void Start()
    {
        StartCoroutine(buildNavMesh());
    }

    IEnumerator buildNavMesh()
    {
        yield return new WaitForSeconds(1);
        Surface2D.BuildNavMesh();
    }
}
