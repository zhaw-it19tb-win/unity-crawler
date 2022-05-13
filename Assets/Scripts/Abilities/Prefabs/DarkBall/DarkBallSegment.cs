using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Abilities.Prefabs.DarkBall {
  public class DarkBallSegment {

    public Transform Effect;
    private GameObject _ability;
    public float effectSpeed = 1f;
    private Vector2 _moveVector;
    private Vector3 _moveVector3d;
    private AbilityRigidbodyCollision _collisionDetector;
    private float destroyEffectInSeconds = 15f;
    public bool IsCasting;
    public Vector2 origin;
    public Vector2 target;
    public float CriticalChance;

    public float targetOffset = 0f;

    public void Cast() {
      IsCasting = true;
      _moveVector = Quaternion.Euler(0, 0, targetOffset) * ((target - origin).normalized * 2f);
      _moveVector3d = new Vector3(_moveVector.x, _moveVector.y, 0) * effectSpeed;
      _ability = UnityEngine.Object.Instantiate(Effect, origin + (0.1f * _moveVector), Quaternion.identity).gameObject;
      UnityEngine.Object.Destroy(_ability, destroyEffectInSeconds);
      _collisionDetector = _ability.GetComponent<AbilityRigidbodyCollision>();
    }

    public float fireRate = 0.05f;
    private float nextFire = 0.0f;
    private float travelModifier = 1f;
    private float damageIncreaseByDistance = 1f;
    // Update is called once per frame
    public void UpdateCast() {
      if (IsCasting) {
        if (_ability == null) {
          IsCasting = false;
          return;
        }

        if (Time.time > nextFire) {
          nextFire = Time.time + fireRate;
          travelModifier += 0.04f;
          damageIncreaseByDistance += 0.25f;
          _ability.transform.localScale += new Vector3(0.001f, 0.001f, 0.001f);
        }
        
        Vector3 newPositionVector3 = _ability.transform.position + (_moveVector3d * Time.deltaTime);
        _ability.transform.position = new Vector3(newPositionVector3.x, newPositionVector3.y, -1);
        
        if (_collisionDetector.hasCollided) {
          Collider2D[] colliders = Physics2D.OverlapCircleAll(_ability.transform.position, 0.2f * travelModifier);
          foreach (Collider2D collider in colliders) {
            if (collider.CompareTag("Player")) {
              collider.gameObject.GetComponent<Health>()?.IncreaseHealth(3 + (int)damageIncreaseByDistance);
            }
            else {
              bool isCrit = UnityEngine.Random.value <= CriticalChance;
              collider.gameObject.GetComponent<Health>()?.TakeDamage(
                isCrit ? (int)(1.4 * (3 + damageIncreaseByDistance)) : (3 + (int)damageIncreaseByDistance), isCrit);
            }
          }
          var impact = GameObject.Instantiate(Effect, _ability.transform.position, Quaternion.identity);
          UnityEngine.Object.Destroy(_ability);
          impact.transform.localScale += new Vector3(0.03f * travelModifier, 0.03f * travelModifier, 0.03f * travelModifier);
          UnityEngine.Object.Destroy(impact.gameObject, 3f);
        }
      }
    }
  }
}
