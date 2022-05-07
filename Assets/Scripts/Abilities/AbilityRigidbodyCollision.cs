using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityRigidbodyCollision : MonoBehaviour
{
  public bool hasCollided = false;

  private void OnCollisionEnter2D(Collision2D collision) {
    if (collision.gameObject.tag != "Player" && collision.gameObject.tag != "Ability") {
      hasCollided = true;
    }
  }

}
