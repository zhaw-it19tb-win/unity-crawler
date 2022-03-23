using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;

public class ballscript : MonoBehaviour
{

    private Rigidbody rigidBody;
    private Vector3 startPos;

    public int springConstant; // N/m
    public float radius; // N/m

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
        float forceX = radius * Mathf.Cos(rigidBody.position.x); // N
        float forceZ = radius * Mathf.Sin(rigidBody.position.z); // N

        rigidBody.AddForce(new Vector3(forceX, 0f, forceZ));

        currentTimeStep += Time.deltaTime;
        timeSeries.Add(new List<float>() {currentTimeStep, rigidBody.position.x, rigidBody.position.z, rigidBody.velocity.x, rigidBody.velocity.z, forceX, forceZ});
    }

    void OnApplicationQuit() {
        WriteTimeSeriesToCSV();
    }

    void WriteTimeSeriesToCSV() {
        using (var streamWriter = new StreamWriter("time_series.csv")) {
            streamWriter.WriteLine("t,x(t),z(t),v_x(t),v_z(t),a_x(t),a_z(t) (added)");
            
            foreach (List<float> timeStep in timeSeries) {
                streamWriter.WriteLine(string.Join(",", timeStep));
                streamWriter.Flush();
            }
        }
    }
}
