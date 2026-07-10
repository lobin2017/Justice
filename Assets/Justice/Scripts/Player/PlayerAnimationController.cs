using UnityEngine;

namespace Player
{
    public enum PlayerState
    {
        Idle,
        Walk,
        Dead
    }

    [RequireComponent(typeof(Animator))]
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

            if (animator == null)
            {
                Debug.LogError($"{name} : Animator가 없습니다.");
            }
        }

        public void UpdateAnimation(Vector2 direction, PlayerState state)
        {
            if (animator == null)
                return;

            if (direction.sqrMagnitude > 0.01f)
            {
                lastDirection = GetDirection(direction);
            }

            string animationName = $"{state}_{lastDirection}";

            if (animationName == currentAnimationState)
                return;

            currentAnimationState = animationName;
            animator.Play(currentAnimationState);
        }

        private string GetDirection(Vector2 direction)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            if (angle < 0f)
                angle += 360f;

            int index = Mathf.RoundToInt(angle / 45f) % 8;

            return DirectionTable[index];
        }
    }
}