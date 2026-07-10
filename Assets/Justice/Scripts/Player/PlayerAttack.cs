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

        private float lastAttackTime;

        private Vector2 aimDirection;

        private PlayerInputActions inputActions;
        private Camera mainCamera;
        private BossManager bossManager;

        private void Awake()
        {
            inputActions = new PlayerInputActions();

            mainCamera = Camera.main;
            bossManager = BossManager.Instance;
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
            UpdateAimDirection();

            if (inputActions.Player.Attack.IsPressed())
            {
                TryAttack();
            }
        }

        private void UpdateAimDirection()
        {
            if (mainCamera == null)
                return;

            Vector2 mousePosition =
                inputActions.Player.Aim.ReadValue<Vector2>();

            Vector2 worldPosition =
                mainCamera.ScreenToWorldPoint(mousePosition);

            aimDirection =
                (worldPosition - (Vector2)transform.position).normalized;
        }

        private void TryAttack()
        {
            if (Time.time < lastAttackTime + weaponDelay)
                return;

            lastAttackTime = Time.time;

            PerformAttack();
        }

        private void PerformAttack()
        {
            if (bossManager == null)
                return;

            BossHealth boss = bossManager.TargetBoss;

            if (boss == null)
                return;

            Vector2 toBoss =
                (boss.transform.position - transform.position);

            float distance = toBoss.sqrMagnitude;

            if (distance > attackDistance * attackDistance)
                return;

            toBoss.Normalize();

            float dot =
                Vector2.Dot(aimDirection, toBoss);

            float cos =
                Mathf.Cos((attackAngle * 0.5f) * Mathf.Deg2Rad);

            if (dot < cos)
                return;

            boss.TakeDamage(weaponDamage);

            Debug.Log($"{boss.name}에게 {weaponDamage} 데미지");
        }
    }
}