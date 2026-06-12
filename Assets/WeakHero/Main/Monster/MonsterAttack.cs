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

        public void Awake()
        {
            monsterSight = GetComponent<MonsterSight>();
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
            IDamageable damageable = player.GetComponent<IDamageable>();
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