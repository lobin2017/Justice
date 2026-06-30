using UnityEngine;

namespace Boss
{
    public enum BossState
    {
        Idle,
        Chase,
        Attack
    }

    public class BossSight : MonoBehaviour
    {
        [SerializeField] public Transform player;

        [Header("거리")]
        [SerializeField] private float chaseDistance = 12f;
        [SerializeField] private float attackDistance = 2.8f;

        private BossState currentState = BossState.Idle;
        private BossState previousState;

        private void Update()
        {
            UpdateState();
        }

        private void UpdateState()
        {
            if (player == null)
            {
                currentState = BossState.Idle;
                return;
            }

            Vector3 toPlayer = player.position - transform.position;
            float sqrDistance = toPlayer.sqrMagnitude;

            Vector3 directionToPlayer = new Vector3(toPlayer.x, 0, toPlayer.z).normalized;

            bool inAttackRange = sqrDistance <= attackDistance * attackDistance;
            bool inChaseRange = sqrDistance <= chaseDistance * chaseDistance;

            if (inAttackRange)
                currentState = BossState.Attack;
            else if (inChaseRange)
                currentState = BossState.Chase;
            else
                currentState = BossState.Idle;

            if (previousState != currentState)
            {
                // Debug.Log($"{gameObject.name}: {previousState} -> {currentState}");
                previousState = currentState;
            }
        }

        public BossState CurrentState => currentState;
    }
}