using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Components
{
  public class RoundShooting : MonoBehaviour, IBossAttack {
    public Transform firePoint;
    [FormerlySerializedAs("Target")] public Transform target;
    public GameObject bulletPrefab;
    public float bulletForce = 1f;
    [FormerlySerializedAs("HowMany")] public int howMany;

    public void Shoot() {
      Debug.Log(firePoint.position);
      Vector3 modifiedFirePoint = Vector3.MoveTowards(firePoint.position, target.position, 0.5f);
      
      
      for (var deg = 0; deg <= 360; deg += 360 / howMany)
      {
        var rad = (deg / 360.0) * Math.PI * 2.0;
        var bullet = Instantiate(bulletPrefab, modifiedFirePoint, firePoint.rotation);
        var direction = new Vector2((float)Math.Sin(rad), (float)Math.Cos(rad)).normalized;

        var component = bullet.GetComponent<Rigidbody2D>();
        component.AddForce(direction * bulletForce, ForceMode2D.Impulse);
      }
    }
  }
}
