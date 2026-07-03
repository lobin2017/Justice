using UnityEngine;

namespace Boss
{
    public class BossHealth : MonoBehaviour
    {
        private float maxHealth;
        private float currentHealth;
        private bool isDead = false;
        private bool isPhase2Triggered = false;

        private BossController controller;
        private BossAnimation animationCtrl;

        private void Awake()
        {
            controller = GetComponent<BossController>();
            animationCtrl = GetComponent<BossAnimation>();
        }

        public void InitializeHealth(float maxHp)
        {
            maxHealth = maxHp;
            currentHealth = maxHealth;
            isDead = false;
            isPhase2Triggered = false;
        }

        public void TakeDamage(float damage)
        {
            if (isDead) return;

            currentHealth -= damage;
            if (animationCtrl != null) animationCtrl.PlayHit();

            if (!isPhase2Triggered && currentHealth <= (maxHealth * 0.5f))
            {
                isPhase2Triggered = true;
                if (controller != null) controller.StartPhaseTransition();
            }

            if (currentHealth <= 0)
            {
                isDead = true;
                currentHealth = 0;
                if (controller != null) controller.HandleDeath();
            }
        }
    }
}