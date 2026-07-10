using UnityEngine;

namespace Boss
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class BossMovement : MonoBehaviour
    {
        private Rigidbody2D rb;
        private float moveSpeed;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        public void Initialize(float speed)
        {
            moveSpeed = speed;
        }

        public void Move(Vector2 direction)
        {
            rb.linearVelocity = direction.normalized * moveSpeed;
        }

        public void Stop()
        {
            rb.linearVelocity = Vector2.zero;
        }
    }
}