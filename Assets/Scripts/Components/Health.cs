using System;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {
  [SerializeField] protected int MaximumHealth = 100;
  [SerializeField] protected int StartHealth = 100;

  [SerializeField] public Slider HealthBar;

  public event Action OnDied;

  public int health { get; private set; } = 0;

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
    if (HealthBar != null) {
      HealthBar.value = health;
    }
    if (health <= 0) {
      Die();
    }
  }

  private void Die() {
    OnDied?.Invoke();
  }
}
