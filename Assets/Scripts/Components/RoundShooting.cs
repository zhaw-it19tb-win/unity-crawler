using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Components
{
  public class RoundShooting : MonoBehaviour, IBossAttack {
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletForce = 1f;
    [FormerlySerializedAs("HowMany")] public int howMany;

    private void Start() {
      var playerObjs = GameObject.FindGameObjectsWithTag("Player");
    }

    public void Shoot() {
      for (var deg = 0; deg <= 360; deg += 360 / howMany)
      {
        var rad = (deg / 360.0) * Math.PI * 2.0;
        var bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        var direction = new Vector2((float)Math.Sin(rad), (float)Math.Cos(rad)).normalized;
        
        Debug.Log(direction);
        
        bullet.GetComponent<Rigidbody2D>().AddForce(direction * bulletForce, ForceMode2D.Impulse);
      }
    }
  }
}
