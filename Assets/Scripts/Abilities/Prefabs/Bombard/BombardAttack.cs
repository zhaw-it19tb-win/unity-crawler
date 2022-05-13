using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombardAttack : Ability {
  public Transform Effect;
  private Transform _skillShotTransform;
  private GameObject _skillShot;
  private Vector2 _moveVector;
  private Vector3 _moveVector3d;
  private AbilityRigidbodyCollision _collisionDetector;

  public override void Cast() {
    IsCasting = true;
    AbilityCurrentlyLockingOtherAbilities = true;
    _skillShotTransform = UnityEngine.Object.Instantiate(Effect, target, Quaternion.identity);
    _skillShot = _skillShotTransform.gameObject;
    UnityEngine.Object.Destroy(_skillShot, destroyEffectInSeconds);
  }

  public float fireRate = 0.25f;
  private float nextFire = 0.0f;
  // Update is called once per frame
  public override void UpdateCast() {
    if (IsCasting) {
      if (_skillShot == null) {
        IsCasting = false;
        AbilityCurrentlyLockingOtherAbilities = false;
        return;
      }
      if (Time.time > nextFire) {
        nextFire = Time.time + fireRate;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_skillShotTransform.position, 0.15f);
        foreach (Collider2D collider in colliders) {
          bool isCrit = UnityEngine.Random.value <= CriticalChance;
          collider.gameObject.GetComponent<Health>()?.TakeDamage(isCrit ? 2 : 1, isCrit);
        }
      }
      
    }
  }
}
