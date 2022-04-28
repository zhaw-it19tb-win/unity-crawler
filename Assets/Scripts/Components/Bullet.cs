using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

  public GameObject bullet;

  // Update is called once per frame
  void OnCollisionEnter2D(Collision2D collision) {
    if (collision.gameObject.GetComponent<Health>() != null) {
      collision.gameObject.GetComponent<Health>().TakeDamage(5);
    }
    Destroy(bullet);
  }
}
