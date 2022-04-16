using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DontRenderColliders : MonoBehaviour
{
    private TilemapRenderer tilemapRender;
    // Start is called before the first frame update

    private void Awake()
    {
        tilemapRender = GetComponent<TilemapRenderer>();
    }
    void Start()
    {
        tilemapRender.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
