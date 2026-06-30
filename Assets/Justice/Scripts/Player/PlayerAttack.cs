using Boss;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    private BossManager bossManager;

    [SerializeField] private float weaponDamage = 5f;
    [SerializeField] private float attackDistance = 3f;
    [SerializeField] private float weaponDelay = 1f;
    [SerializeField] private float attackAngle = 70f;

    private float lastWeaponAttackTime;
    private Vector2 aimDirection;
    private PlayerInputActions inputActions;
    private SpriteRenderer spriteRenderer;
    private Camera mainCamera;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        Vector2 mousePos = inputActions.Player.Aim.ReadValue<Vector2>();
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        aimDirection = (mousePos - (Vector2)transform.position).normalized;

        if (aimDirection.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (aimDirection.x < 0)
        {
            spriteRenderer.flipX = true;
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
        BossManager bossManager = BossManager.Instance;

        if (bossManager == null || bossManager.activeMonsters.Count == 0)
        {

            return;
        }
        for (int i = 0; i < bossManager.activeMonsters.Count; i++)
        {
            BossHealth boss = bossManager.activeMonsters[i];

            if (boss == null)
                continue;

            Vector2 directionToMonster = (boss.transform.position - transform.position);
            float distanceSqr = directionToMonster.sqrMagnitude;
            float attackRangeSqr = attackDistance * attackDistance;

            if (distanceSqr > attackRangeSqr)
            {
                continue;
            }

            directionToMonster.Normalize();
            float dot = Vector2.Dot(aimDirection, directionToMonster.normalized);
            float cosAngle = Mathf.Cos((attackAngle * 0.5f) * Mathf.Deg2Rad);

            if (dot > cosAngle)
            {
                boss.TakeDamage(weaponDamage);
                Debug.Log($"{boss.name} 에게 {weaponDamage} 데미지");
            }
        }
    }
    void Update()
    {
        HandleAiming();
        HandleAttackInput();
    }
}