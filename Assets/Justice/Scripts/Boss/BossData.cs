using UnityEngine;

namespace Boss
{
    public enum BossType
    {
        // --- 7인의 마왕 ---
        Nexar,     // 저주와 사명의 마왕
        Audax,     // 만용과 용기의 마왕
        Furion,    // 광기와 투지의 마왕
        Spelis,    // 절망과 희망의 마왕
        Credis,    // 회의와 신념의 마왕
        Votar,     // 집착과 염원의 마왕
        Delios,    // 환각과 환희의 마왕

        // --- 국왕 및 4가주 ---
        Verian,    // 국왕
        Valten, // 집행 가주
        Serane, // 순명 가주
        Ordel,// 규율 가주
        Verdan    // 충의 가주
    }

    [CreateAssetMenu(fileName = "BossData", menuName = "Boss/Boss Data")]
    public class BossData : ScriptableObject
    {
        [Header("기본 정보")]
        public BossType bossType;
        public string bossName;

        [Header("능력치")]
        [Min(1)]
        public float maxHealth = 100f;

        [Min(0.1f)]
        public float moveSpeed = 3f;

        [Min(0.1f)]
        public float attackRange = 2f;

        [Min(0.1f)]
        public float attackCooldown = 2f;

        [Min(1)]
        public float damage = 10f;

        [Header("페이즈")]
        [Range(0f, 1f)]
        public float phase2Threshold = 0.5f;
    }
}