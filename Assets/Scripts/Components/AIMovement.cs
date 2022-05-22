using UnityEngine;
using UnityEngine.AI;

public class AIMovement : MonoBehaviour
{
    private GameObject target;
    private NavMeshAgent agent;

    public float moveSpeed = 1.0f;
    private Vector2 currentDirection;
    private bool isMovePressed;

    public Transform targetForShooting;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindWithTag("Player");
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        GameObject[] playerObjs = GameObject.FindGameObjectsWithTag("Player");
        targetForShooting = playerObjs[0].GetComponent<Transform>();
    }


    public void Move()
    {
        currentDirection = new Vector2(agent.velocity.x, agent.velocity.y);
        if (target != null)
        {
            agent.SetDestination(target.transform.position);
            GetComponentInChildren<ArcherAnimation>().SetDirection(currentDirection);
        }

    }

    // shoot function 
    public void Shoot()
    {
        Vector2 direction = new Vector2(targetForShooting.transform.position.x - agent.transform.position.x, targetForShooting.transform.position.y - agent.transform.position.y);
        // Animation part
        GetComponentInChildren<ArcherAnimation>().SetAttackDirection(direction);
    }
}

