using UnityEngine;

namespace Boss
{
    public enum BossType
    {
        Nexar,   // 저주와 사명의 마왕
        Audax,   // 만용과 용기의 마왕
        Furion,  // 광기와 투지의 마왕
        Spelis,  // 절망과 희망의 마왕
        Credis,  // 회의와 신념의 마왕
        Votar,   // 집착과 염원의 마왕
        Delios   // 환각과 환희의 마왕
    }

    [CreateAssetMenu(fileName = "NewBossData", menuName = "Boss/Boss Data")]
    public class BossData : ScriptableObject
    {
        [Header("보스 기본 정보")]
        public BossType bossType;
        public string bossName = "마왕 이름";

        [Header("보스 능력치 설정")]
        public float maxHealth = 100f;
        public float moveSpeed = 3f;
        public float attackRange = 2f;
        public float attackCooldown = 2f;
        public float damage = 10f;
    }
}