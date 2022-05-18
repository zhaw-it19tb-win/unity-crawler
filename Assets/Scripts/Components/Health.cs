using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {
  [SerializeField] protected int MaximumHealth = 100;
  [SerializeField] protected int StartHealth = 100;
  [SerializeField] public int CurrentHealth { get; set; } = 0;

  [SerializeField] public Image SliderImage;

  public bool EnableAutoHealthCounter = true;
  public float OffsetHealthBarY = 0.5f;
  public float OffsetHealthBarX = 0.5f;
  public float OffsetHealthWidth = 0f;
  public float OffsetHealthHeight  = 0f;

  public event Action OnDied;


  private void Awake() {
    CurrentHealth = StartHealth;
    UpdateHealth(0);
  }

  public void TakeDamage(int damage, bool isCritical = false, bool producesPopup = true) {
    UpdateHealth(-damage);
    if (producesPopup) {
      DamagePopup.Create(this.gameObject.transform.position, damage, isCritical);
    }
  }

  public void IncreaseHealth(int healing, bool producesPopup = true) {
    UpdateHealth(healing);
    if (producesPopup) {
      DamagePopup.Create(this.gameObject.transform.position, -healing, false);
    }
  }

  private void UpdateHealth(int updateHealth) {
    CurrentHealth += updateHealth;
    if (CurrentHealth <= 0) {
      Die();
    }
    if (CurrentHealth >= MaximumHealth) {
      CurrentHealth = MaximumHealth;
    }
    UpdateUI();
  }
  void OnGUI() {
    if (EnableAutoHealthCounter) {
      Vector2 targetPos;
      targetPos = Camera.main.WorldToScreenPoint(transform.position);
      GUI.Box(new Rect(
        targetPos.x + OffsetHealthBarX,
        Screen.height - targetPos.y + OffsetHealthBarY,
        60 + OffsetHealthWidth,
        20 + OffsetHealthHeight),
        CurrentHealth + "/" + MaximumHealth);
    }
  }

  private void UpdateUI() {
    if (SliderImage != null) {
      SliderImage.fillAmount = (float)CurrentHealth / (float)MaximumHealth;
    }
  }

  private void Die() {
    OnDied?.Invoke();
  }
}
