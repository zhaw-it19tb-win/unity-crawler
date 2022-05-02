using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundAttack : MonoBehaviour
{
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] playerObjs = GameObject.FindGameObjectsWithTag("Player");
        player = playerObjs[0];

    }

    // Update is called once per frame
    void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.transform.position, 1.0f);
        if (Input.GetKeyDown("space"))
        {
            foreach (Collider2D collider in colliders)
            {
                collider.gameObject.GetComponent<Health>()?.TakeDamage(10);
            }
        }
    }

}
