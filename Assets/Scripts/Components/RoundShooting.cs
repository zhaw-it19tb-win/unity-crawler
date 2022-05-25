using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Components
{
  public class RoundShooting : MonoBehaviour, IBossAttack {
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletForce = 1f;
    public float arrowSpawningDistance = 0.4f;
    [FormerlySerializedAs("HowMany")] public int howMany = 15;

    public void Perform() {
      for (var deg = 0; deg < 360; deg += 360 / howMany)
      {
        var rad = (deg / 360.0) * Math.PI * 2.0;
        var bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        var rigidBody = bullet.GetComponent<Rigidbody2D>();
        rigidBody.SetRotation((float)rad);
        var direction = new Vector2((float)Math.Sin(rad), (float)Math.Cos(rad)).normalized;
        rigidBody.position += direction * arrowSpawningDistance;

        rigidBody.AddForce(direction * bulletForce, ForceMode2D.Impulse);
      }
    }
  }
}
