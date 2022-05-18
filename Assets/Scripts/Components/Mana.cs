using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Components {
  public class Mana : MonoBehaviour {
    [SerializeField] protected int MaximumMana = 100;
    [SerializeField] protected int StartMana = 100;

    [SerializeField] protected float ManaRegenPerSecond = 5f;
    private float regenRate = 0.2f;
    private float nextRegen = 0.0f;
    private float manaRegenPerFireRate;

    [SerializeField] protected float ManaRegenStopAfterUse = 2f;
    private float continueRegen = 0.0f;

    [SerializeField] public Image SliderImage;

    public float CurrentMana { get; private set; } = 0;

    private void Awake() {
      UpdateMana(StartMana);
      manaRegenPerFireRate = regenRate * ManaRegenPerSecond; 
    }

    private void Update() {
      if (Time.time > continueRegen) {
        if (Time.time > nextRegen) {
          nextRegen = Time.time + regenRate;
          IncreaseMana(manaRegenPerFireRate);
        }
      }
    }

    public void DecreaseMana(float mana) {
      if (mana > 0) {
        continueRegen = Time.time + ManaRegenStopAfterUse;
        UpdateMana(-mana);
      }
    }

    public void IncreaseMana(float mana) {
      if (mana > 0) {
        UpdateMana(mana);
      }
    }

    private void UpdateMana(float mana) {
      CurrentMana += mana;
      if (CurrentMana >= MaximumMana) {
        CurrentMana = MaximumMana;
      }
      UpdateUI();
    }

    private void UpdateUI() {
      if (SliderImage != null) {
        SliderImage.fillAmount = (float)CurrentMana / (float)MaximumMana;
      }
    }

  }
}
