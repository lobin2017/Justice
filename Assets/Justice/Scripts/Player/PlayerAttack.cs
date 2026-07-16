using Core;
using UnityEngine;
using GameManager;

namespace Player
{
    public class PlayerAttack : MonoBehaviour
    {
        [Header("Attack Settings")]
        [SerializeField] private float weaponDamage = 10f;
        [SerializeField] private float attackDistance = 3f;
        [SerializeField] private float attackDelay = 0.5f;
        [SerializeField] private float attackAngle = 70f;

        private float lastAttackTime;

        private Camera mainCamera;
        private PlayerInputActions inputActions;

        private Vector2 aimDirection;

        private void Awake()
        {
            inputActions = PlayerInput.Instance.Actions;
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
            UpdateAimDirection();

            if (inputActions.Player.Attack.WasPressedThisFrame())
            {
                if (Time.time >= lastAttackTime + attackDelay)
                {
                    lastAttackTime = Time.time;
                    PerformAttack();
                }
            }
        }

        private void UpdateAimDirection()
        {
            if (mainCamera == null)
                return;

            Vector2 mousePos = inputActions.Player.Aim.ReadValue<Vector2>();

            Vector2 worldPos =
                mainCamera.ScreenToWorldPoint(mousePos);

            aimDirection =
                (worldPos - (Vector2)transform.position).normalized;
        }

        private void PerformAttack()
        {
            if (BossManager.Instance == null)
                return;

            BossHealth boss = BossManager.Instance.TargetBoss;

            if (boss == null)
                return;

            Vector2 toBoss =
                (boss.transform.position - transform.position);

            if (toBoss.sqrMagnitude >
                attackDistance * attackDistance)
                return;

            toBoss.Normalize();

            float dot =
                Vector2.Dot(aimDirection, toBoss);

            float limit =
                Mathf.Cos((attackAngle * 0.5f) * Mathf.Deg2Rad);

            if (dot < limit)
                return;

            boss.TakeDamage(weaponDamage);

            Debug.Log($"{boss.name}에게 {weaponDamage} 피해");
        }
    }
}