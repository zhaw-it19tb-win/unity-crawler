using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Movement))]
public class PlayerInputController : MonoBehaviour {

  private Health health;
  private Movement movement;

  void Awake() {
    health = GetComponent<Health>();
    movement = GetComponent<Movement>();
  }

  // Update is called once per frame
  void Update() {
    //movement.Move(new Vector3(Random.Range(-11, 11), Random.Range(-11, 11), 0));
  }
}
