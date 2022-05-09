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

  private bool isAttacking = false;
  private bool attackAnimationFinished = false;
  // TODO: this is a bad solution
  private float attackTime = 1f; //s
  private float passedAttackTime = 0f; //s
  void Start() {
        health.OnDied += OnDied;
  }

  void Awake() {
        health = GetComponent<Health>();
        shooting = GetComponent<Shooting>();
        aiMovement = GetComponent<AIMovement>();
        InvokeRepeating(nameof(ToggleAttack), 0, 1);
    }

    void ToggleAttack() {
        isAttacking = !isAttacking; 
    }

    // Update is called once per frame
  private void Update()
  {
        if (!isAttacking)
        {
            aiMovement.Move();
        }
        else if (isAttacking && passedAttackTime <= attackTime) {
            aiMovement.Shoot();
            passedAttackTime += Time.deltaTime;
            if (passedAttackTime >= attackTime) {
                passedAttackTime = 0f;
                shooting.Shoot();
            }
            
        }
  }

  void FixedUpdate() {

  }

  private void OnDied()
  {
        Destroy(this.gameObject);
  }
}
