using UnityEngine;

namespace Boss
{
    public enum BossState { Idle, Chase, Attack, PhaseTransition, Death }

    public class BossController : MonoBehaviour
    {
        [Header("기획 데이터 장착")]
        public BossData bossData; 

        private BossState currentState = BossState.Idle;
        private Transform playerTransform;

        private BossSight sight;
        private BossAttack attack;
        private BossHealth health;
        private BossAnimation animationCtrl;
        private BossMovement movement;

        private void Awake()
        {
            sight = GetComponent<BossSight>();
            attack = GetComponent<BossAttack>();
            health = GetComponent<BossHealth>();
            animationCtrl = GetComponent<BossAnimation>();
            movement = GetComponent<BossMovement>();
        }

        private void Start()
        {
            if (bossData != null)
            {
                health.InitializeHealth(bossData.maxHealth);
                attack.InitializeAttack(bossData.attackRange, bossData.attackCooldown, bossData.damage);

                if (movement != null)
                    movement.Initialize(bossData.moveSpeed);
            }
        }

        private void Update()
        {
            if (currentState == BossState.Death || currentState == BossState.PhaseTransition) return;

            if (playerTransform == null && sight != null)
            {
                playerTransform = sight.GetDetectedPlayer();
            }

            if (playerTransform == null)
            {
                SetState(BossState.Idle);
            }
            else
            {
                float distance = Vector3.Distance(transform.position, playerTransform.position);
                Vector3 direction = (playerTransform.position - transform.position).normalized;

                if (distance <= bossData.attackRange)
                {
                    SetState(BossState.Attack);
                }
                else
                {
                    SetState(BossState.Chase);

                    if (movement != null)
                        movement.Move(direction);
                }
            }

            ExecuteStateBehavior();
        }

        private void SetState(BossState newState)
        {
            if (currentState == newState) return;
            currentState = newState;
        }

        private void ExecuteStateBehavior()
        {
            if (currentState == BossState.Attack && attack != null && playerTransform != null)
            {
                attack.TryAttack(playerTransform);
            }
        }

        public void StartPhaseTransition()
        {
            SetState(BossState.PhaseTransition);
            if (animationCtrl != null) animationCtrl.PlayPhaseTransition();

            Debug.Log($"{bossData.bossName}이(가) 2페이즈로 각성합니다!");
        }

        public void HandleDeath()
        {
            SetState(BossState.Death);
            if (animationCtrl != null) animationCtrl.PlayDeath();
            Debug.Log($"{bossData.bossName}이(가) 처단되었습니다.");
        }

        public BossType GetBossType() => bossData != null ? bossData.bossType : BossType.Nexar;
    }
}