using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{

    public double health = 100.0;

    private Rigidbody rigidBody;
    private Vector3 startPos;
    private float currentTimeStep; // s
    
    private List<List<float>> timeSeries;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float playerSpeed = 20.0f;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;

    private float forceX;
    private float forceY;
    private float forceZ;

    private void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();
        rigidBody = GetComponent<Rigidbody>();
        startPos = rigidBody.position;
        timeSeries = new List<List<float>>();
    }

    // Update is called once per frame
    void Update()
    {

    }        
    
    void FixedUpdate()
    {
        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            forceY += Mathf.Sqrt(10.0f * -3.0f * -9.81f);
        } else {
            forceY = 0;
        }
        forceX = Input.GetAxis("Horizontal") * 10.0f; 
        forceZ = Input.GetAxis("Vertical") * 10.0f; 

        rigidBody.AddForce(new Vector3(forceX, forceY, forceZ));

        currentTimeStep += Time.deltaTime;
        timeSeries.Add(new List<float>() {currentTimeStep, rigidBody.position.x, rigidBody.position.z, rigidBody.velocity.x, rigidBody.velocity.z, forceX, forceZ});
    }
    
}
