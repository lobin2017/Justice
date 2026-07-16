using UnityEngine;
using GameManager;

namespace Core
{
    [RequireComponent(typeof(BossSight))]
    [RequireComponent(typeof(BossMovement))]
    [RequireComponent(typeof(BossAttack))]
    [RequireComponent(typeof(BossHealth))]
    [RequireComponent(typeof(BossAnimation))]
    public class BossController : MonoBehaviour
    {
        [Header("Boss Data")]
        [SerializeField] private BossData bossData;

        private BossState currentState = BossState.Idle;

        private Transform player;

        private BossSight sight;
        private BossMovement movement;
        private BossAttack attack;
        private BossHealth health;
        private BossAnimation animationController;

        private float currentMoveSpeed;
        private float currentDamage;

        public BossType BossType => bossData.bossType;
        public BossData BossData => bossData;

        private bool phase2 = false;

        private void Awake()
        {
            sight = GetComponent<BossSight>();
            movement = GetComponent<BossMovement>();
            attack = GetComponent<BossAttack>();
            health = GetComponent<BossHealth>();
            animationController = GetComponent<BossAnimation>();

            currentMoveSpeed = bossData.moveSpeed;
            currentDamage = bossData.damage;
        }

        private void Start()
        {
            if (bossData == null)
            {
                Debug.LogError($"{name} : BossData가 지정되지 않았습니다.");
                enabled = false;
                return;
            }

            movement.Initialize(currentMoveSpeed);

            attack.Initialize(
                bossData.attackRange,
                bossData.attackCooldown,
                currentDamage);

            health.Initialize(bossData.maxHealth);
        }

        private void Update()
        {
            if (currentState == BossState.Death ||
                currentState == BossState.PhaseTransition)
                return;

            player = sight.DetectedPlayer;
            if (player == null)
            {
                ChangeState(BossState.Idle);
            }
            else
            {
                float distance =
                    Vector2.Distance(transform.position, player.position);

                if (distance <= attack.AttackRange)
                {
                    ChangeState(BossState.Attack);
                }
                else
                {
                    ChangeState(BossState.Chase);
                }
            }

            ExecuteState();

            Vector2 direction = Vector2.zero;

            if (player != null)
            {
                direction =
                    (player.position - transform.position).normalized;
            }

            animationController.UpdateAnimation(currentState, direction);
        }

        private void ExecuteState()
        {
            switch (currentState)
            {
                case BossState.Idle:

                    movement.Stop();

                    break;

                case BossState.Chase:

                    Vector2 direction =
                        (player.position - transform.position).normalized;

                    movement.Move(direction);

                    break;

                case BossState.Attack:

                    movement.Stop();
                    attack.TryAttack(player);

                    break;
            }
        }

        private void ChangeState(BossState newState)
        {
            if (currentState == newState)
                return;

            currentState = newState;
        }

        public void StartPhaseTransition()
        {
            if (phase2)
                return;

            phase2 = true;

            ChangeState(BossState.PhaseTransition);

            animationController.PlayPhaseTransition();

            currentMoveSpeed *= 1.3f;
            currentDamage *= 1.5f;    

            movement.Initialize(currentMoveSpeed);

            Invoke(nameof(EndPhaseTransition), 1.5f);
        }
        private void EndPhaseTransition()
        {
            ChangeState(BossState.Chase);
        }

        public void HandleDeath()
        {
            ChangeState(BossState.Death);

            if (TryGetComponent<HouseBossController>(out var houseBoss))
            {
                houseBoss.Die();

                return;
            }

            BossManager.Instance?.ClearBoss();

            animationController.PlayDeath();

            BossManager.Instance?.OnBossKilled(this);

            Destroy(gameObject, 1.5f);
        }
    }
}