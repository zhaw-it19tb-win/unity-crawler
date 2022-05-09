using Components;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Controller
{
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(Shooting))]
    [RequireComponent(typeof(AIMovement))]
    public class BasicBossController : MonoBehaviour
    {
        private Health _health;
        private Shooting _shooting;
        private AIMovement _aiMovement;
        private IBossAttack[] _bossAttacks;

        private bool _isAttacking;

        // TODO: this is a bad solution
        private const float AttackTime = 1f; //s
        private float _passedAttackTime; //s

        private const int NormalAttacksCount = 1;
        private int _attacksUntilBossAttackLeft = NormalAttacksCount;

        private void Start()
        {
            _health.OnDied += OnDied;
        }

        private void Awake()
        {
            _health = GetComponent<Health>();
            _shooting = GetComponent<Shooting>();
            _aiMovement = GetComponent<AIMovement>();
            _bossAttacks = GetComponents<IBossAttack>();
            InvokeRepeating(nameof(ToggleAttack), 0, 1);
        }

        private void ToggleAttack()
        {
            _isAttacking = !_isAttacking;
        }

        private void FixedUpdate()
        {
            if (!_isAttacking)
            {
                _aiMovement.Move();
            }
            else if (_isAttacking && _passedAttackTime <= AttackTime)
            {
                _aiMovement.Shoot();
                _passedAttackTime += Time.deltaTime;
                if (!(_passedAttackTime >= AttackTime)) return;

                _passedAttackTime = 0f;
                if (_attacksUntilBossAttackLeft == 0)
                {
                    DoBossAttack();
                }
                else
                {
                    _shooting.Shoot();
                }
                _attacksUntilBossAttackLeft--;
                if (_attacksUntilBossAttackLeft < 0)
                {
                    _attacksUntilBossAttackLeft = NormalAttacksCount;
                }
            }
        }

        private void DoBossAttack()
        {
            var bossAttack = _bossAttacks[Random.Range(0, _bossAttacks.Length - 1)];
            bossAttack.Shoot();
        }

        private void OnDied()
        {
            Destroy(gameObject);
        }
    }
}
