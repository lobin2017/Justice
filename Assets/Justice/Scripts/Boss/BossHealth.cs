using UnityEngine;

namespace Boss
{
    public class BossHealth : MonoBehaviour, IDamageable
    {
        public float MaxHp { get; private set; }
        public float CurrentHp { get; private set; }

        private bool isDead;
        private bool phase2Triggered;

        private BossController controller;
        private BossAnimation animationController;

        private void Awake()
        {
            controller = GetComponent<BossController>();
            animationController = GetComponent<BossAnimation>();
        }

        public void Initialize(float maxHp)
        {
            MaxHp = maxHp;
            CurrentHp = maxHp;

            isDead = false;
            phase2Triggered = false;

            BossManager.Instance?.RegisterBoss(this);
        }

        public void TakeDamage(float damage)
        {
            if (isDead)
                return;

            CurrentHp -= damage;

            animationController?.PlayHit();

            if (!phase2Triggered && CurrentHp <= MaxHp * 0.5f)
            {
                phase2Triggered = true;
                controller.StartPhaseTransition();
            }

            if (CurrentHp <= 0)
            {
                CurrentHp = 0;
                Die();
            }
        }

        public void Die()
        {
            if (isDead)
                return;

            isDead = true;

            controller.HandleDeath();
        }
    }
}