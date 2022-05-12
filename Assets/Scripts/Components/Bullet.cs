using UnityEngine;

namespace Components
{
    public class Bullet : MonoBehaviour {
        public GameObject bullet;

        private void OnCollisionEnter2D(Collision2D collision) {
            if (collision.gameObject.GetComponent<Health>() != null) {
                collision.gameObject.GetComponent<Health>().TakeDamage(5);
            }
            Destroy(bullet);
        }
    }
}