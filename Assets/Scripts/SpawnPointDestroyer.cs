using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointDestroyer : MonoBehaviour
{
    // Called on collision
    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("RoomSpawnPoint")) {
            Destroy(other.gameObject);
        }
    }
}
