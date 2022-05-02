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

    public Transform firePoint;
    public Transform targetForShooting;
    public GameObject bulletPrefab;
    public float bulletForce = 2f;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindWithTag("PlayerChild");
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        GameObject[] playerObjs = GameObject.FindGameObjectsWithTag("Player");
        targetForShooting = playerObjs[0].GetComponent<Transform>();
        //InvokeRepeating("Shoot", 0, 1);
    }

    /*
    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            agent.SetDestination(target.transform.position);
        }

        currentDirection = new Vector2(agent.velocity.x, agent.velocity.y);
        FindObjectOfType<ArcherAnimation>().SetDirection(currentDirection);
    }
    */

    public void Move() {
        currentDirection = new Vector2(agent.velocity.x, agent.velocity.y);
        if (target != null)
        {
            agent.SetDestination(target.transform.position);
            FindObjectOfType<ArcherAnimation>().SetDirection(currentDirection);
        }
       
    }

    // shoot function 
    public void Shoot()
    {
        Vector3 modifiedFirePoint = Vector3.MoveTowards(firePoint.position, targetForShooting.position, 0.5f);
        GameObject bullet = Instantiate(bulletPrefab, modifiedFirePoint, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        Vector2 direction = (target.transform.position - modifiedFirePoint).normalized;
        rb.AddForce(direction * bulletForce, ForceMode2D.Impulse);
        //FindObjectOfType<AudioManager>().Play("Shot");

        // Animation part
        agent.SetDestination(target.transform.position);
        FindObjectOfType<ArcherAnimation>().SetAttackDirection(currentDirection);

    }
}

    