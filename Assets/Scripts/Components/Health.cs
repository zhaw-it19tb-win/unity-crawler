using System;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {
  [SerializeField] protected int MaximumHealth = 100;
  [SerializeField] protected int StartHealth = 100;

  [SerializeField] public Slider HealthBar;
  [SerializeField] public Image SliderImage;

  public event Action OnDied;

  public int health { get; private set; } = 0;

  private void Awake() {
    health = StartHealth;
    if (HealthBar != null) {
      HealthBar.maxValue = MaximumHealth;
      HealthBar.minValue = 0;
    }
    SetHealth(StartHealth);
  }

  public void TakeDamage(int damage) {
    health -= damage;
    if (health <= 0) {
      SetHealth(0);
      Die();
    } 
    else {
      SetHealth(health);
    }
  }

  private void SetHealth(int updateHealth) {
    if (HealthBar != null) {
      HealthBar.value = updateHealth;
    }
    if (SliderImage != null) {
      Debug.Log("update to health: " + updateHealth + " // " + MaximumHealth);
      SliderImage.fillAmount = (float)updateHealth / (float)MaximumHealth;
    }
  }

    public void IncreaseHealth(int healing) {
        health += healing;
        if (health > 100) {
            health = 100;
        }
        // HealthBar.value = health; -> health bar currently not working cause its null.
    }

  private void Die() {
    OnDied?.Invoke();
  }
}
