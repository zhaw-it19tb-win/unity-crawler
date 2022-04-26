using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchShooting : MonoBehaviour
{
    public Transform firePoint;
    private Vector3 target;
    public GameObject bulletPrefab;
    public float bulletForce;

    // Update is called once per frame
    void Start()
    {
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.z = 0.0f;
            Vector3 modifiedFirePoint = Vector3.MoveTowards(firePoint.position, target, 0.1f);
            Debug.Log("modifiedFirePoint: " + modifiedFirePoint);
            Debug.Log("firePoint: " + firePoint.position);
            GameObject bullet = Instantiate(bulletPrefab, modifiedFirePoint, firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            Vector2 direction = (target - modifiedFirePoint).normalized;
            rb.AddForce(direction * bulletForce, ForceMode2D.Impulse);
        }
    }
}
