using UnityEngine;

namespace Components
{
    public class Striking : MonoBehaviour, IAttack
    {
        public float requiredHitDistance = 0.75f;
        public int damage = 5;
        private Transform target;
        private Transform self;
        private Health targetHealth;

        private void Start()
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            target = player.GetComponent<Transform>();
            self = GetComponent<Transform>();
            targetHealth = player.GetComponent<Health>();
        }

        public void Perform()
        {
            var distance = (target.position - self.position).magnitude;
            Debug.Log(distance);
            if (distance > requiredHitDistance) return;
            
            targetHealth.TakeDamage(damage);
        }
    }
}
