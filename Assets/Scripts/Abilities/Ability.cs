using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour {

  /*
   An ability is a mono behaviour, but only actually instaniated when it is being fired
   The script might be attached to the effect prefab, so do not count on methods like Update()
   */

  public Sprite Icon;
  public float effectSpeed = 1f;
  public float destroyEffectInSeconds = 15f;
  public float cooldownInSeconds = 4f;
  public Vector2 origin;
  public Vector2 target;

  public bool IsCasting = false;
  public bool AbilityCurrentlyLockingOtherAbilities = false;

  /// <summary>Casts the ability</summary>
  public bool TryCast(Vector2 from, Vector2 to) {
    if (IsReady()) {
      Debug.Log("casting");
      origin = from;
      target = to;
      UseCooldown();
      Cast();
      return true;
    }
    else {
      return false;
    }
  }

  /// <summary>Casts the ability</summary>
  public abstract void Cast();

  /// <summary>Casts the ability</summary>
  public abstract void UpdateCast();

  // Cooldown management


  private float _timeNextCast = 0f;
  public float GetCurrentCooldown() => IsReady() ? 0 : _timeNextCast - Time.time;
  public float GetCooldownPercentage() => IsReady() ? 1 : (cooldownInSeconds - (_timeNextCast - Time.time)) / cooldownInSeconds;

  public bool IsReady() {
    Debug.Log("time.time " + Time.time);
    Debug.Log("_timeNextCast " + _timeNextCast);
    Debug.Log("cooldownInSeconds " + cooldownInSeconds);
    return Time.time >= _timeNextCast;
  }


  public void UseCooldown() {
    _timeNextCast = Time.time + cooldownInSeconds;
  }
}
