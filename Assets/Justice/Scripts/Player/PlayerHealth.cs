using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    private float maxHp = 200f;

    public float MaxHp => maxHp;
    public float CurrentHp { get; private set; }

    private bool isDead;

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

        Debug.Log("플레이어 사망");

        gameObject.SetActive(false);
    }
}