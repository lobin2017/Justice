using UnityEngine;

namespace Boss
{
    [RequireComponent(typeof(BossController))]
    [RequireComponent(typeof(BossAnimation))]
    public class BossHealth : MonoBehaviour, IDamageable
    {
        private float maxHp;
        private float currentHp;

        private bool isDead;
        private bool phase2Triggered;

        private BossController controller;
        private BossAnimation animationController;

        public float MaxHp => maxHp;
        public float CurrentHp => currentHp;

        private void Awake()
        {
            controller = GetComponent<BossController>();
            animationController = GetComponent<BossAnimation>();
        }

        public void Initialize(float hp)
        {
            maxHp = hp;
            currentHp = hp;

            isDead = false;
            phase2Triggered = false;
        }

        public void TakeDamage(float damage)
        {
            if (isDead)
                return;

            currentHp -= damage;
            currentHp = Mathf.Clamp(currentHp, 0f, maxHp);

            animationController.PlayHit();

            if (!phase2Triggered &&
                currentHp <= maxHp * controller.BossData.phase2Threshold)
            {
                phase2Triggered = true;
                controller.StartPhaseTransition();
            }

            if (currentHp <= 0f)
            {
                Die();
            }
        }

        public void Die()
        {
            if (isDead)
                return;

            isDead = true;

            animationController.PlayDeath();
            controller.HandleDeath();
        }
    }
}