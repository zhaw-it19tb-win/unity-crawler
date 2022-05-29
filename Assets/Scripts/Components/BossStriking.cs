using JetBrains.Annotations;
using UnityEngine;

namespace Components
{
    public class BossStriking : MonoBehaviour, IBossAttack
    {
        public float requiredHitDistance = 1.5f;
        public int damage = 10;
        private Transform _target;
        private Transform _self;
        private Health _targetHealth;

        private void Start()
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            _target = player.GetComponent<Transform>();
            _self = GetComponent<Transform>();
            _targetHealth = player.GetComponent<Health>();
        }

        public void Perform()
        {
            var distance = (_target.position - _self.position).magnitude;
            if (distance > requiredHitDistance) return;
            
            _targetHealth.TakeDamage(damage);
        }
    }
}
