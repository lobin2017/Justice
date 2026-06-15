using Monster;
using UnityEngine;
using System.Collections.Generic;

public class MonsterManager : MonoBehaviour
{
    public List<MonsterHealth> monsters = new List<MonsterHealth>();

    void Start()
    {
        Debug.Log("몬스터 출현");
        foreach (MonsterHealth monster in monsters)
        {
            Debug.Log($"{monster.name} 체력 : {monster.CurrentHp}");
        }
    }
    void Update()
    {
        for (int i = monsters.Count - 1; i >= 0; i--)
        {
            if (monsters[i].CurrentHp <= 0)
            {
                monsters.RemoveAt(i);
                Debug.Log($"남은 몬스터 수 : {monsters.Count}");
            }
            if (monsters.Count <= 0)
            {
                Debug.Log("몬스터 토벌 완료!");
            }
        }
    }
}
