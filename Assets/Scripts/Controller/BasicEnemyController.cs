using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Shooting))]
public class BasicEnemyController : MonoBehaviour {
  private Health health;
  private Shooting shooting;

  void Awake() {
    health = GetComponent<Health>();
    shooting = GetComponent<Shooting>();
  }

  void FixedUpdate() {

  }
}
