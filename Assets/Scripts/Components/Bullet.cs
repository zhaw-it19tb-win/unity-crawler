using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject bullet;

  public GameObject bullet;
  
    void OnCollisionEnter2D(Collision2D collision) {
   
    if (collision.gameObject.GetComponent<Health>() != null) {
      collision.gameObject.GetComponent<Health>().TakeDamage(5);
    }
}
