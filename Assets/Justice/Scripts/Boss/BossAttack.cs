using UnityEngine;

namespace Boss
{
    public class BossAttack : MonoBehaviour
    {
        private float range;
        private float cooldown;
        private float damage;
        private float lastAttackTime;

        private BossController controller;
        private BossAnimation animationCtrl;

        private void Awake()
        {
            controller = GetComponent<BossController>();
            animationCtrl = GetComponent<BossAnimation>();
        }

        public void InitializeAttack(float attackRange, float attackCooldown, float attackDamage)
        {
            range = attackRange;
            cooldown = attackCooldown;
            damage = attackDamage;
        }

        public void TryAttack(Transform target)
        {
            if (Time.time >= lastAttackTime + cooldown)
            {
                lastAttackTime = Time.time;

                // 1. 공격 애니메이션 시각 신호 발동
                if (animationCtrl != null) animationCtrl.PlayAttackTrigger();

                // 2. 현재 장착된 마왕 타입에 맞춰 특수 패턴 실행
                if (controller != null)
                {
                    ExecutePatternByBossType(controller.GetBossType(), target);
                }
            }
        }

        private void ExecutePatternByBossType(BossType type, Transform target)
        {
            // ⭐ 감독님이 연출하고 싶은 마왕별 공격 로직이 작성될 분기 구역입니다!
            switch (type)
            {
                case BossType.Nexar:
                    Debug.Log("넥사르: 플레이어를 향해 돌진 공격!");
                    // TODO: 돌진 컴포넌트 실행 및 target에 데미지 전달
                    break;

                case BossType.Audax:
                    Debug.Log("아우닥스: 유도 불꽃 투사체 발사!");
                    break;

                case BossType.Furion:
                    Debug.Log("퓨리온: 주변 광역 포효 지진 공격!");
                    break;

                case BossType.Spelis:
                    Debug.Log("스펠리스: 마법 진을 소환하여 번개 소환!");
                    break;

                case BossType.Credis:
                    Debug.Log("크레디스: 전방 칼날 부메랑 투척!");
                    break;

                case BossType.Votar:
                    Debug.Log("보타르: 플레이어의 뒤로 순간이동 후 기습!");
                    break;

                case BossType.Delios:
                    Debug.Log("델리오스: 체력을 흡수하는 암흑 구체 발사!");
                    break;
            }

            // 기본 근접 판정 처리 예시 (IDamageable 등 연동용)
            // target.GetComponent<IDamageable>()?.TakeDamage(damage);
        }
    }
}