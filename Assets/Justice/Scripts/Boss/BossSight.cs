using UnityEngine;

namespace Boss
{
    public class BossSight : MonoBehaviour
    {
        [Header("시야 범위 설정")]
        public float viewRadius = 7f;         // 플레이어를 감지할 원형 시야 반경
        public LayerMask playerLayer;         // 플레이어 오브젝트의 레이어 (예: Player)

        private Transform detectedPlayer;

        private void Update()
        {
            // 주기적으로 시야 범위 내에 playerLayer를 가진 콜라이더가 있는지 체크
            Collider2D targetCollider = Physics2D.OverlapCircle(transform.position, viewRadius, playerLayer);

            if (targetCollider != null)
            {
                // 플레이어 레이어를 가진 오브젝트를 찾았다면 감지 대상으로 등록
                detectedPlayer = targetCollider.transform;
            }
            else
            {
                // 범위를 벗어났다면 참조를 비워둠
                detectedPlayer = null;
            }
        }

        // ⭐ BossController가 실시간으로 "플레이어 찾았어?" 하고 호출할 메서드
        public Transform GetDetectedPlayer()
        {
            return detectedPlayer;
        }

        // 유니티 에디터 화면(Scene 뷰)에서 기획자가 시야 범위를 눈으로 볼 수 있게 해주는 기믹
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, viewRadius);
        }
    }
}