using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour {

  public Transform firePoint;
  public Transform target;
  public GameObject bulletPrefab;
  public float bulletForce = 20f;

  // Update is called once per frame
  void Start() {
    GameObject[] playerObjs = GameObject.FindGameObjectsWithTag("Player");
    target = playerObjs[0].GetComponent<Transform>();

    InvokeRepeating(nameof(Shoot), 0, 1);
  }

  void Shoot() {
    GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
    Vector2 direction = (target.transform.position - firePoint.transform.position).normalized;
    rb.AddForce(direction * bulletForce, ForceMode2D.Impulse);
    FindObjectOfType<AudioManager>().Play("Shot");
  }
}
