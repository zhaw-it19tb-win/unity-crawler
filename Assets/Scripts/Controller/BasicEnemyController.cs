using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Shooting))]
[RequireComponent(typeof(AIMovement))]
public class BasicEnemyController : MonoBehaviour {
  private Health health;
  private Shooting shooting;
  private AIMovement aiMovement;

  void Awake() {
        health = GetComponent<Health>();
        shooting = GetComponent<Shooting>();
        aiMovement = GetComponent<AIMovement>();
  }

  void FixedUpdate() {

  }
}
