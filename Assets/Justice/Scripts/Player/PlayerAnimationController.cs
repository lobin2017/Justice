using UnityEngine;

namespace Player
{
    public enum PlayerState
    {
        Idle,
        Walk,
        Dead
    }

    public class PlayerAnimationController : MonoBehaviour
    {
        private Animator animator;
        private string currentAnimationState;

        private string lastDirection = "S";

        private static readonly string[] DirectionTable =
        {
            "E", "NE", "N", "NW",
            "W", "SW", "S", "SE"
        };

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public void UpdateAnimation(Vector2 direction, PlayerState state)
        {
            if (direction.sqrMagnitude > 0.01f)
            {
                lastDirection = GetDirection(direction);
            }

            string newAnimation = $"{state}_{lastDirection}";

            if (currentAnimationState == newAnimation)
                return;

            currentAnimationState = newAnimation;
            animator.Play(currentAnimationState);
        }

        private string GetDirection(Vector2 direction)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            if (angle < 0)
                angle += 360f;

            int index = Mathf.FloorToInt((angle + 22.5f) / 45f) % 8;

            return DirectionTable[index];
        }
    }
}