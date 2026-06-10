using Portfolio;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Transform enemy;

    [SerializeField] private float weaponDamage = 5f;
    [SerializeField] private float attackDistance = 2.5f;

    public void OnAttack()
    {
        if (Keyboard.current.oKey.wasPressedThisFrame)
        {
            PerformAttack();
        }
    }
    public void PerformAttack()
    {
        Vector3 paDistance = enemy.position - transform.position;
        float sqrDistance = paDistance.sqrMagnitude;
        float attackSqr = attackDistance * attackDistance;

        if (sqrDistance < attackSqr)
        {
            IDamageable damageable = enemy.GetComponent<IDamageable>();
            damageable.TakeDamage(weaponDamage);
            Debug.Log($"{gameObject.name}의 공격");
        }
        else
        {
            Debug.Log($"{gameObject.name}공격 범위 밖");
        }
    }
    void Update()
    {
        OnAttack();

    }
}