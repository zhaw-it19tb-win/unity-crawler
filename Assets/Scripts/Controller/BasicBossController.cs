using System.Collections;
using Components;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Controller
{
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(IAttack))]
    [RequireComponent(typeof(AIMovement))]
    public class BasicBossController : MonoBehaviour
    {
        private Health _health;
        private IAttack _attack;
        private AIMovement _aiMovement;
        private IBossAttack[] _bossAttacks;
        private ParticleSystem _particleSystem;

        private bool _isAttacking;

        // TODO: this is a bad solution
        private const float AttackTime = 1f; //s
        private float _passedAttackTime; //s

        private const int NormalAttacksCount = 5;
        private int _attacksUntilBossAttackLeft = NormalAttacksCount;

        private void Start()
        {
            _health.OnDied += OnDied;
        }

        private void Awake()
        {
            _health = GetComponent<Health>();
            _attack = GetComponent<IAttack>();
            _aiMovement = GetComponent<AIMovement>();
            _bossAttacks = GetComponents<IBossAttack>();
            _particleSystem = GetComponent<ParticleSystem>();
            InvokeRepeating(nameof(ToggleAttack), 0, 2);
        }

        private void ToggleAttack()
        {
            _isAttacking = true;
            StartCoroutine(MakeAttack());
        }

        private IEnumerator MakeAttack()
        {
            var isBossAttack = _attacksUntilBossAttackLeft == 0;
            _aiMovement.Shoot();
            if (isBossAttack)
            {
                _particleSystem.Play();
            }
            yield return new WaitForSeconds(AttackTime);

            if (isBossAttack)
            {
                DoBossAttack();
            }
            else
            {
                _attack.Perform();
            }

            _attacksUntilBossAttackLeft--;
            if (_attacksUntilBossAttackLeft < 0)
            {
                _attacksUntilBossAttackLeft = NormalAttacksCount;
            }

            _isAttacking = false;
        }

        private void FixedUpdate()
        {
            if (!_isAttacking)
            {
                _aiMovement.Move();
            }
        }

        private void DoBossAttack()
        {
            var bossAttack = _bossAttacks[Random.Range(0, _bossAttacks.Length - 1)];
            bossAttack.Perform();
        }

        private void OnDied()
        {
            Destroy(gameObject);
        }
    }
}
