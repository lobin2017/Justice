using UnityEngine;

namespace Boss
{
    public class BossSight : MonoBehaviour
    {
        [Header("시야 범위 설정")]
        public float viewRadius = 7f;         
        public LayerMask playerLayer;         

        private Transform detectedPlayer;

        private void Update()
        {
            Collider2D targetCollider = Physics2D.OverlapCircle(transform.position, viewRadius, playerLayer);

            if (targetCollider != null)
            {
                detectedPlayer = targetCollider.transform;
            }
            else
            {
                detectedPlayer = null;
            }
        }

        public Transform GetDetectedPlayer()
        {
            return detectedPlayer;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, viewRadius);
        }
    }
}