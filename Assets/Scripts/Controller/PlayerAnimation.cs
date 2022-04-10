using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public string[] staticDirections = { "Static_N", "Static_NW", "Static_W", "Static_SW", "Static_S", "Static_SE", "Static_E", "Static_NE" };
    public string[] runDirections = { "Run_N", "Run_NW", "Run_W", "Run_SW", "Run_S", "Run_SE", "Run_E", "Run_NE" };
    private Animator anim;
    // Start is called before the first frame update

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
   
    }
}
