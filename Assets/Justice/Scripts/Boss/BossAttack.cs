using UnityEngine;

namespace Boss
{
    public class MonsterAttack : MonoBehaviour
    {
        [SerializeField] public Transform player;

        [SerializeField] private float attackDamage = 5f;
        [SerializeField] private float attackCooldown = 2f;
        private float lastAttackTime;
        private BossSight bossSight;
        private PlayerHealth playerHealth;

        private void Awake()
        {
            bossSight = GetComponent<BossSight>();
        }
        public void Initialize(Transform playerTransform)
        {
            player = playerTransform;
            if (player != null)
            {
                playerHealth = player.GetComponent<PlayerHealth>();
            }
        }
        public void OnAttack()
        {
            if (bossSight.CurrentState == BossState.Attack)
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
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage);
                Debug.Log($"{gameObject.name}의 공격");
            }
        }
        void Update()
        {
            OnAttack();
        }
    }
}