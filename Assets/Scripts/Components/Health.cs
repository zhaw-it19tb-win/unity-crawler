using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {
  [SerializeField]
  protected int MaximumHealth = 100;
  [SerializeField]
  protected int StartHealth = 100;

  [SerializeField]
  protected Slider HealthBar = null;

  public event Action OnDied;

  private int health = 0;

  private void Awake() {
    health = StartHealth;
    if (HealthBar != null) {
      HealthBar.maxValue = MaximumHealth;
      HealthBar.value = StartHealth;
      HealthBar.minValue = 0;
    }
  }

  public void TakeDamage(int damage) {
    health -= damage;
    Debug.Log("damage taken, health: " + health);
    //HealthBar.value = health;
    if (health <= 0) {
      Die();
    }
  }

  private void Die() {
    if (OnDied != null) {
            Debug.Log("On Died != null");
      OnDied();
    }
  }

}
