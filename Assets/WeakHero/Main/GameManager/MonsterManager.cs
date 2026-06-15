using Monster;
using UnityEngine;
using System.Collections.Generic;

public class MonsterManager : MonoBehaviour
{
    public List<MonsterHealth> monsters = new List<MonsterHealth>();
    
    void Start()
    {
        foreach (MonsterHealth monster in monsters)
        {
            print($"{monster.name} 체력 : {monster.CurrentHp}");
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
        }
    }
}
