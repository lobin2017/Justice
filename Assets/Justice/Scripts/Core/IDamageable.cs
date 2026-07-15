public interface IDamageable
{
    float CurrentHp { get; }
    float MaxHp { get; }
    void TakeDamage(float damageAmount);
    void Die();
}