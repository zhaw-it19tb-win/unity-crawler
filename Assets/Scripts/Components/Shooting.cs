using UnityEngine;

namespace Components
{
  public class Shooting : MonoBehaviour, IAttack {
    public Transform firePoint;
    public Transform target;
    public GameObject bulletPrefab;
    public float bulletForce = 20f;

    // Update is called once per frame
    void Start() {
      GameObject[] playerObjs = GameObject.FindGameObjectsWithTag("Player");
      target = playerObjs[0].GetComponent<Transform>();
    }

    public void Perform()
    {
      Vector3 modifiedFirePoint = Vector3.MoveTowards(firePoint.position, target.position, 0.5f);
      GameObject bullet = Instantiate(bulletPrefab, modifiedFirePoint, firePoint.rotation);
      Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
      Vector2 direction = (target.transform.position - modifiedFirePoint).normalized;
      rb.AddForce(direction * bulletForce, ForceMode2D.Impulse);
    }
  }
}
