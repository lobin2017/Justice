using UnityEngine;

namespace Boss
{
    public class BossMovement : MonoBehaviour
    {
        private float moveSpeed;
        private Transform self;

        private void Awake()
        {
            self = transform;
        }

        public void Initialize(float speed)
        {
            moveSpeed = speed;
        }

        public void Move(Vector3 direction)
        {
            if (direction == Vector3.zero) return;

            self.position += direction.normalized * moveSpeed * Time.deltaTime;
        }
    }
}