using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine;
using UnityEngine.InputSystem;

public class AIMovement : MonoBehaviour
{
    private GameObject target;
    private NavMeshAgent agent;

    public float moveSpeed = 1.0f;
    private Vector2 currentDirection;
    private bool isMovePressed;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindWithTag("PlayerChild");
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            agent.SetDestination(target.transform.position);
        }

        currentDirection = new Vector2(agent.velocity.x, agent.velocity.y);
        //Debug.Log("agent x and y " + agent.desiredVelocity.x + "  "+ agent.desiredVelocity.y);
        //Debug.Log("CurrentDirection = " + currentDirection);
        FindObjectOfType<ArcherAnimation>().SetDirection(currentDirection);
    }
    /*
    public void OnMovement(InputAction.CallbackContext context)
    {
        currentMovement = context.ReadValue<Vector2>();
        isMovePressed = currentMovement.x != 0 || currentMovement.y != 0;

        float currentSpeed = isMovePressed ? moveSpeed : 0.0f;
    }*/
}
