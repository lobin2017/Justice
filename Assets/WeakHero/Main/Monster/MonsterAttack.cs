using UnityEngine;

namespace Monster
{
    public class MonsterAttack : MonoBehaviour
    {
        [SerializeField] private Transform player;

        [SerializeField] private float attackDamage = 5f;
        [SerializeField] private float attackCooldown = 2f;
        private float lastAttackTime;
        private MonsterSight monsterSight;
        private IDamageable damageable;

        public void Awake()
        {
            monsterSight = GetComponent<MonsterSight>();
            damageable = GetComponent<IDamageable>();
        }
        public void OnAttack()
        {
            if (monsterSight.CurrentState == MonsterState.Attack)
            {
                if (Time.time - lastAttackTime >= attackCooldown)
                {
                    PerformAttack();
                    lastAttackTime = Time.time;
                }
            }
        }
        public void PerformAttack()
        {
            if (damageable != null)
            {
                damageable.TakeDamage(attackDamage);
                Debug.Log($"{gameObject.name}의 공격");
            }
        }
        void Update()
        {
            OnAttack();
        }
    }
}