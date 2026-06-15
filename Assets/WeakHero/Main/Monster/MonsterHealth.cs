using UnityEngine;
using System;

namespace Monster
{
    public class MonsterHealth : MonoBehaviour, IDamageable
    {
        [SerializeField] private float maxHp = 20f;
        public float MaxHp => maxHp;
        public float CurrentHp { get; set; }

        public event Action OnDeath;

        private bool isDead = false;

        private void Awake()
        {
            CurrentHp = maxHp;
        }

        public void TakeDamage(float damageAmount)
        {
            if (isDead) return;

            CurrentHp -= damageAmount;
            CurrentHp = Mathf.Clamp(CurrentHp, 0, maxHp);

            Debug.Log($"{gameObject.name}이(가) {damageAmount}의 피해를 입었습니다. 남은 HP: {CurrentHp}");

            if (CurrentHp <= 0)
            {
                Die();
            }
        }

        public void Die()
        {
            if (isDead) return;
            isDead = true;

            Debug.Log($"{gameObject.name}이(가) 사망했습니다.");

            OnDeath?.Invoke();

            Destroy(gameObject, 0.5f);
        }
    }
}