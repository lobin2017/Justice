using Boss;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerAttack : MonoBehaviour
    {
        [Header("Attack Settings")]
        [SerializeField] private float weaponDamage = 5f;
        [SerializeField] private float attackDistance = 3f;
        [SerializeField] private float weaponDelay = 1f;
        [SerializeField] private float attackAngle = 70f;

        private float lastWeaponAttackTime;
        private Vector2 aimDirection;

        private PlayerInputActions inputActions;
        private Camera mainCamera;

        private void Awake()
        {
            inputActions = new PlayerInputActions();
            mainCamera = Camera.main;
        }

        private void OnEnable()
        {
            inputActions.Player.Enable();
        }

        private void OnDisable()
        {
            inputActions.Player.Disable();
        }

        private void Update()
        {
            HandleAiming();
            HandleAttackInput();
        }

        private void HandleAiming()
        {
            if (mainCamera == null) return;

            Vector2 mousePos = inputActions.Player.Aim.ReadValue<Vector2>();
            Vector3 worldMousePos = mainCamera.ScreenToWorldPoint(mousePos);

            aimDirection = ((Vector2)worldMousePos - (Vector2)transform.position).normalized;
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

            if (bossManager == null || bossManager.TargetBoss == null) return;

            BossHealth boss = bossManager.TargetBoss;

            Vector2 directionToMonster = (boss.transform.position - transform.position);
            float distanceSqr = directionToMonster.sqrMagnitude;
            float attackRangeSqr = attackDistance * attackDistance;

            if (distanceSqr > attackRangeSqr) return;

            directionToMonster.Normalize();
            float dot = Vector2.Dot(aimDirection, directionToMonster);
            float cosAngle = Mathf.Cos((attackAngle * 0.5f) * Mathf.Deg2Rad);

            if (dot > cosAngle)
            {
                boss.TakeDamage(weaponDamage);
                Debug.Log($"{boss.name}에게 {weaponDamage} 데미지!");
            }
        }
    }
}