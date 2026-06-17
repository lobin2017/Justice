using Monster;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private MonsterManager monsterManager;

    [SerializeField] private float weaponDamage = 5f;
    [SerializeField] private float attackDistance = 3f;
    [SerializeField] private float weaponDelay = 1f;
    [SerializeField] private float attackAngle = 70f;
    [SerializeField] private float slerpSpeed = 8f;

    private float lastWeaponAttackTime;
    private PlayerInputActions inputActions;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }
    private void OnEnable()
    {
        inputActions.Player.Enable();
    }
    private void OnDisable()
    {
        inputActions.Player.Disable();
    }
    private void HandleAiming()
    {
        Vector2 mouseScreenPos = inputActions.Player.Aim.ReadValue<Vector2>();
        Ray ray = Camera.main.ScreenPointToRay(mouseScreenPos);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 mouseDir = hit.point - transform.position;
            mouseDir.y = 0;

            if (mouseDir.sqrMagnitude > 0.1f)
            {
                Quaternion targetRot = Quaternion.LookRotation(mouseDir);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, slerpSpeed * Time.deltaTime);
            }
        }
    }
    private void HandleAttackInput()
    {
        if (inputActions.Player.Attack.IsPressed())
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
        if (monsterManager == null || monsterManager.monsters.Count == 0)

            return;

        foreach (MonsterHealth monster in monsterManager.monsters)
        {
            if (monster == null) continue;

            Vector3 directionToMonster = monster.transform.position - transform.position;
            float distanceSqr = directionToMonster.sqrMagnitude;
            float attackRangeSqr = attackDistance * attackDistance;

            if (distanceSqr > attackRangeSqr)
                continue;

            directionToMonster.Normalize();
            float dot = Vector3.Dot(transform.forward, directionToMonster);
            float cosAngle = Mathf.Cos(attackAngle * Mathf.Deg2Rad);

            if (dot > cosAngle)
            {
                monster.TakeDamage(weaponDamage);
                Debug.Log($"{monster.name} 에게 {weaponDamage} 데미지");
            }
        }

    }
    void Update()
    {
        HandleAiming();
        HandleAttackInput();
    }
}