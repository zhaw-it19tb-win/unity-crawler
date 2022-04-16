using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this class designates where a 10x10 room prefab should be instantiated
public class RoomSpawner : MonoBehaviour
{
    /*
    1 -> need bottom door, use templates.roomsD
    2 -> need left door, use templates.roomsL
    3 -> need top door, use templates.roomsT
    4 -> need right door, use templates.roomsR
    */
    public int openingDirection;
    
    private RoomTemplates templates;
    private int rand;
    private bool spawned = false; 

    // Start is called before the first frame update
    void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        
        Invoke("Spawn", 0.1f);
        
    }

    void Spawn()
    {
        if (!spawned) 
        {
            if (openingDirection == 1)
            {
                GameObject[] roomTemplates = templates.roomsD;
                rand = Random.Range(0,  roomTemplates.Length); 
                Instantiate( roomTemplates[rand], transform.position,  roomTemplates[rand].transform.rotation );
            } else if (openingDirection == 2)
            {
                GameObject[] roomTemplates = templates.roomsL;
                rand = Random.Range(0,  roomTemplates.Length); 
                Instantiate( roomTemplates[rand], transform.position,  roomTemplates[rand].transform.rotation );
            } else if (openingDirection == 3)
            {
                GameObject[] roomTemplates = templates.roomsT;
                rand = Random.Range(0,  roomTemplates.Length); 
                Instantiate( roomTemplates[rand], transform.position,  roomTemplates[rand].transform.rotation );
            } else if (openingDirection == 4)
            {
                GameObject[] roomTemplates = templates.roomsR;
                rand = Random.Range(0,  roomTemplates.Length); 
                Instantiate( roomTemplates[rand], transform.position,  roomTemplates[rand].transform.rotation );
            }
            spawned = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("RoomSpawnPoint"))
        {
            Destroy(gameObject);
        }
    }
}
