using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;

public class ballscript : MonoBehaviour
{
    public float radius = 5.0f;
    private float forceX;
    private float forceZ;
    public double health = 100.0;
    private Rigidbody rigidBody;
    private Vector3 startPos;
    private float currentTimeStep; // s
    
    private List<List<float>> timeSeries;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        startPos = rigidBody.position;
        timeSeries = new List<List<float>>();
    }

    // Update is called once per frame
    void Update()
    {
            
    }

    // FixedUpdate can be called multiple times per frame
    void FixedUpdate() {

        radius = 3F;
        forceX = radius * Mathf.Cos(rigidBody.position.x);
        forceZ = radius * Mathf.Sin(rigidBody.position.z);

        rigidBody.AddForce(new Vector3(forceX, 0f, forceZ));

        currentTimeStep += Time.deltaTime;
        timeSeries.Add(new List<float>() {currentTimeStep, rigidBody.position.x, rigidBody.position.z, rigidBody.velocity.x, rigidBody.velocity.z, forceX, forceZ});
    }

}
