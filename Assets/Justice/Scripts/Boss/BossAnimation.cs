using UnityEngine;

namespace Boss
{
    public enum BossState
    {
        Idle,
        Chase,
        Attack,
        PhaseTransition,
        Death
    }
    [RequireComponent(typeof(Animator))]
    public class BossAnimation : MonoBehaviour
    {
        private Animator animator;

        private static readonly int IsMovingHash = Animator.StringToHash("isMoving");
        private static readonly int AttackHash = Animator.StringToHash("attack");
        private static readonly int HitHash = Animator.StringToHash("hit");
        private static readonly int DeathHash = Animator.StringToHash("death");
        private static readonly int PhaseHash = Animator.StringToHash("phaseTransition");

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public void UpdateAnimation(BossState state, Vector2 direction)
        {
            if (animator == null) return;

            animator.SetBool(IsMovingHash, state == BossState.Chase);
        }

        public void PlayAttack()
        {
            animator.SetTrigger(AttackHash);
        }

        public void PlayHit()
        {
            animator.SetTrigger(HitHash);
        }

        public void PlayDeath()
        {
            animator.SetTrigger(DeathHash);
        }

        public void PlayPhaseTransition()
        {
            animator.SetTrigger(PhaseHash);
        }
    }
}