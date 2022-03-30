using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
  [SerializeField]
  protected float movementSpeed = 1.0f;

  public void Move(Vector3 movement) {
    transform.position += movement * Time.deltaTime * movementSpeed;
  }

}
