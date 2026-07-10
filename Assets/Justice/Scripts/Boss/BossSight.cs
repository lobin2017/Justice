using UnityEngine;

namespace Boss
{
    public class BossSight : MonoBehaviour
    {
        [SerializeField] private float viewRadius = 7f;
        [SerializeField] private LayerMask playerLayer;

        public Transform DetectedPlayer { get; private set; }

        private void Update()
        {
            Collider2D target =
                Physics2D.OverlapCircle(transform.position, viewRadius, playerLayer);

            DetectedPlayer = target != null ? target.transform : null;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, viewRadius);
        }
    }
}