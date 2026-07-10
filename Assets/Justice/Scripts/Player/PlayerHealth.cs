using UnityEngine;

namespace Player
{
    public class PlayerHealth : MonoBehaviour, IDamageable
    {
        [Header("Health Settings")]
        [SerializeField] private float maxHp = 200f;

        public float MaxHp => maxHp;
        public float CurrentHp { get; private set; }

        public bool IsDead => isDead;

        private bool isDead;

        private void Awake()
        {
            Initialize();
        }

        public void Initialize()
        {
            CurrentHp = maxHp;
            isDead = false;
        }

        public void TakeDamage(float damageAmount)
        {
            if (isDead)
                return;

            CurrentHp -= damageAmount;
            CurrentHp = Mathf.Clamp(CurrentHp, 0f, maxHp);

            Debug.Log($"{name}이(가) {damageAmount}의 피해를 입었습니다. 남은 HP : {CurrentHp}");

            if (CurrentHp <= 0f)
            {
                Die();
            }
        }

        public void Heal(float amount)
        {
            if (isDead)
                return;

            CurrentHp += amount;
            CurrentHp = Mathf.Clamp(CurrentHp, 0f, maxHp);
        }

        public void Die()
        {
            if (isDead)
                return;

            isDead = true;

            Debug.Log("플레이어 사망");

            gameObject.SetActive(false);
        }
    }
}