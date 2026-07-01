using UnityEngine;

namespace Boss
{
    public enum BossState { Idle, Chase, Attack, PhaseTransition, Death }

    public class BossController : MonoBehaviour
    {
        [Header("기획 데이터 장착")]
        public BossData bossData; // 인스펙터에서 원하는 마왕 데이터를 넣어줍니다.

        private BossState currentState = BossState.Idle;
        private Transform playerTransform;

        // 컴포넌트 참조
        private BossSight sight;
        private BossAttack attack;
        private BossHealth health;
        private BossAnimation animationCtrl;

        private void Awake()
        {
            sight = GetComponent<BossSight>();
            attack = GetComponent<BossAttack>();
            health = GetComponent<BossHealth>();
            animationCtrl = GetComponent<BossAnimation>();
        }

        private void Start()
        {
            // 장착된 마왕 데이터가 있다면 시스템에 초기 스펙 반영
            if (bossData != null)
            {
                health.InitializeHealth(bossData.maxHealth);
                attack.InitializeAttack(bossData.attackRange, bossData.attackCooldown, bossData.damage);
            }
        }

        private void Update()
        {
            if (currentState == BossState.Death || currentState == BossState.PhaseTransition) return;

            // 1. 플레이어 감지 여부 체크 (Sight 연동)
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
                // 플레이어와의 거리 및 방향 계산
                float distance = Vector3.Distance(transform.position, playerTransform.position);
                Vector3 direction = (playerTransform.position - transform.position).normalized;

                // 애니메이션 방향 회전 및 이동 상태 업데이트
                if (animationCtrl != null) animationCtrl.UpdateAnimation(currentState, direction);

                // 2. 사거리 기반 상태 변경
                if (distance <= bossData.attackRange)
                {
                    SetState(BossState.Attack);
                }
                else
                {
                    SetState(BossState.Chase);
                    // 이동 속도는 데이터 시트의 속도를 반영
                    transform.position += direction * bossData.moveSpeed * Time.deltaTime;
                }
            }

            // 3. 상태별 행동 수행
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

        // Health 스크립트에서 페이즈 전환 트리거 발생 시 호출
        public void StartPhaseTransition()
        {
            SetState(BossState.PhaseTransition);
            if (animationCtrl != null) animationCtrl.PlayPhaseTransition();

            // TODO: 페이즈 연출(약 2초 대기 등)이 끝난 후 다시 Chase 상태로 돌리는 로직 추가 가능
            Debug.Log($"{bossData.bossName}이(가) 2페이즈로 각성합니다!");
        }

        // Health 스크립트에서 사망 시 호출
        public void HandleDeath()
        {
            SetState(BossState.Death);
            if (animationCtrl != null) animationCtrl.PlayDeath();
            Debug.Log($"{bossData.bossName}이(가) 처단되었습니다.");
        }

        public BossType GetBossType() => bossData != null ? bossData.bossType : BossType.Nexar;
    }
}