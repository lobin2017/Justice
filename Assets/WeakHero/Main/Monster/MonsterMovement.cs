using Monster;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    [SerializeField] private Transform player;

    [SerializeField] private float moveSpeed = 3f;
    private MonsterSight monsterSight;

    private void Awake()
    {
        monsterSight = GetComponent<MonsterSight>();
    }
    public void OnChase()
    {
        if (monsterSight.CurrentState == MonsterState.Chase)
        {
            Vector3 distance = player.position - gameObject.transform.position;
            Vector3 moveDir = distance.normalized;

            transform.Translate(moveDir * moveSpeed * Time.deltaTime, Space.World);
        }
    }

    void Update()
    {
        OnChase();
    }
}

