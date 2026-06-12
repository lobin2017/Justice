using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Transform enemy;

    [SerializeField] private float weaponDamage = 5f;
    [SerializeField] private float attackDistance = 3f;
    [SerializeField] private float weaponDelay = 1f;
    [SerializeField] private float sightAngle = 45f;

    private IDamageable damageable;
    private float lastWeaponAttackTime;
    private Vector3 mouseDir;

    Ray ray;
    RaycastHit hit;

    public void Awake()
    {
        damageable = enemy.GetComponent<IDamageable>();
    }
    public void UpdateMouseCamera()
    {
        ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        Physics.Raycast(ray, out hit);

        mouseDir = hit.point - transform.position;

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Quaternion playerLook = Quaternion.LookRotation(mouseDir);
            transform.rotation = playerLook;
        }
    }
    public void OnAttack()
    {
        if (Mouse.current.leftButton.isPressed)
        {
            if (Time.time - lastWeaponAttackTime >= weaponDelay)
            {
                PerformAttack();
                lastWeaponAttackTime = Time.time;
            }
        }
    }
    public void PerformAttack()
    {
        Vector3 paDistance = enemy.position - transform.position;
        float sqrDistance = paDistance.sqrMagnitude;
        float attackSqr = attackDistance * attackDistance;

        Vector3 dirToEnemy = paDistance.normalized;
        float dot = Vector3.Dot(transform.forward, dirToEnemy);
        float sightThreshold = Mathf.Cos(sightAngle * Mathf.Deg2Rad);

        bool inAttackAngle = dot > sightThreshold;

        if (sqrDistance < attackSqr && inAttackAngle)
        {
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
        UpdateMouseCamera();
        OnAttack();
    }
}