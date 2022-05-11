using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Abilities {
  public class FireballAttack : Ability {
    public Transform Effect;
    private Transform _skillShotTransform;
    private GameObject _skillShot;
    private Vector2 _moveVector;
    private Vector3 _moveVector3d;
    private AbilityRigidbodyCollision _collisionDetector;
    public bool blocksOtherAbilities = false;

    protected void Start() {
      //cooldown.CooldownInSeconds = 5f;
    }

    public override void Cast() {
      IsCasting = true;
      if (blocksOtherAbilities) {
        AbilityCurrentlyLockingOtherAbilities = true;
      }
      _skillShotTransform = UnityEngine.Object.Instantiate(Effect, origin, Quaternion.identity);
      _skillShot = _skillShotTransform.gameObject;
      UnityEngine.Object.Destroy(_skillShot, destroyEffectInSeconds);
      _moveVector = (target - origin).normalized * effectSpeed;
      _moveVector3d = new Vector3(_moveVector.x, _moveVector.y, 0);
      _collisionDetector = _skillShotTransform.gameObject.GetComponent<AbilityRigidbodyCollision>();
    }

    // Update is called once per frame
    public override void UpdateCast() {
      if (IsCasting) {
        if (_skillShot == null) {
          IsCasting = false;
          AbilityCurrentlyLockingOtherAbilities = false;
          return;
        }
        Vector3 newPositionVector3 = _skillShotTransform.position + _moveVector3d * (Time.deltaTime);
        _skillShotTransform.position = new Vector3(newPositionVector3.x, newPositionVector3.y, -1);
        if (_collisionDetector.hasCollided) {
          Collider2D[] colliders = Physics2D.OverlapCircleAll(_skillShotTransform.position, 0.2f);
          foreach (Collider2D collider in colliders) {
            collider.gameObject.GetComponent<Health>()?.TakeDamage(10);
          }
          var impact = GameObject.Instantiate(Effect, _skillShotTransform.position, Quaternion.identity);
          UnityEngine.Object.Destroy(_skillShot);
          impact.transform.localScale += new Vector3(0.03f, 0.03f, 0.03f);
          UnityEngine.Object.Destroy(impact.gameObject, 3f);
        }
      }
    }
  }
}
