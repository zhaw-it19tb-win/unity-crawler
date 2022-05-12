using System;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {
  [SerializeField] protected int MaximumHealth = 100;
  [SerializeField] protected int StartHealth = 100;

  [SerializeField] public Slider HealthBar;
  [SerializeField] public Image SliderImage;

  public event Action OnDied;

  public int _health { get; private set; } = 0;

  private void Awake() {
    _health = StartHealth;
    if (HealthBar != null) {
      HealthBar.maxValue = MaximumHealth;
      HealthBar.minValue = 0;
    }
    UpdateHealth(0);
  }

  public void TakeDamage(int damage) {
    UpdateHealth(-damage);
  }

  public void IncreaseHealth(int healing) {
    UpdateHealth(healing);
  }

  private void UpdateHealth(int updateHealth) {
    if (_health <= 0) {
      Die();
    }
    else {
      _health += updateHealth;
    }
    if (_health >= MaximumHealth) {
      _health = MaximumHealth;
    }
    UpdateUI();
  }

  private void UpdateUI() {
    if (HealthBar != null) {
      HealthBar.value = _health;
    }
    if (SliderImage != null) {
      Debug.Log("update to health: " + _health + " // " + MaximumHealth);
      SliderImage.fillAmount = (float)_health / (float)MaximumHealth;
    }
  }

  private void Die() {
    OnDied?.Invoke();
  }
}
