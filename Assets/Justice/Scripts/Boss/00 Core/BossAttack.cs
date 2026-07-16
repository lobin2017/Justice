using UnityEngine;

namespace Core
{
    [RequireComponent(typeof(BossController))]
    [RequireComponent(typeof(BossAnimation))]
    public class BossAttack : MonoBehaviour
    {
        private float attackRange;
        private float attackCooldown;
        private float damage;

        private float lastAttackTime;

        private BossController controller;
        private BossAnimation animationController;

        public float AttackRange => attackRange;

        private void Awake()
        {
            controller = GetComponent<BossController>();
            animationController = GetComponent<BossAnimation>();
        }

        public void Initialize(float range, float cooldown, float attackDamage)
        {
            attackRange = range;
            attackCooldown = cooldown;
            damage = attackDamage;
        }

        public void TryAttack(Transform target)
        {
            if (target == null)
                return;

            if (Time.time < lastAttackTime + attackCooldown)
                return;

            lastAttackTime = Time.time;

            animationController.PlayAttack();

            ExecutePattern(controller.BossType, target);
        }

        private void ExecutePattern(BossType bossType, Transform target)
        {
            switch (bossType)
            {
                case BossType.Nexar:
                    // TODO : 저주
                    break;

                case BossType.Audax:
                    // TODO : 만용
                    break;

                case BossType.Furion:
                    // TODO : 광기
                    break;

                case BossType.Spelis:
                    // TODO : 절망
                    break;

                case BossType.Credis:
                    // TODO : 회의
                    break;

                case BossType.Votar:
                    // TODO : 집착
                    break;

                case BossType.Delios:
                    // TODO : 환각
                    break;
            }

            if (Vector2.Distance(transform.position, target.position) <= attackRange)
            {
                IDamageable damageable = target.GetComponent<IDamageable>();

                if (damageable != null)
                {
                    damageable.TakeDamage(damage);
                }
            }
        }
    }
}