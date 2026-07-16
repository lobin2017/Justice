using UnityEngine;

namespace Player
{
    public enum HouseType
    {
        None,
        Blindness,   // 맹목
        Obedience,   // 순명
        Order,       // 규율
        Loyalty      // 충의
    }

    public class PlayerStatus : MonoBehaviour
    {
        public static PlayerStatus Instance { get; private set; }

        [Header("현재 계승한 가문의 힘")]
        [SerializeField] private HouseType currentHouse = HouseType.None;
        public HouseType CurrentHouse => currentHouse;

        [Header("맹목")]
        [SerializeField] private float blindnessCooldown = 60f;
        private float lastBlindnessTime = -100f;

        [Header("충의")]
        [SerializeField] private float loyaltyCooldown = 4f;
        [SerializeField] private float staminaReturnAmount = 15f;
        private float lastLoyaltyTime = -100f;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void InscribeHouseSign(HouseType newHouse)
        {
            currentHouse = newHouse;

            Debug.Log(
                $"<color=green>[정의의 재해석]</color> {currentHouse} 가문의 힘을 계승했습니다.");
        }

        public float CalculateMitigatedDamage(float damage)
        {
            if (currentHouse != HouseType.Blindness)
                return damage;

            if (Time.time < lastBlindnessTime + blindnessCooldown)
                return damage;

            lastBlindnessTime = Time.time;

            float reducedDamage = damage * 0.7f;

            Debug.Log(
                $"<color=cyan>[맹목]</color> 피해 감소 ({damage} → {reducedDamage})");

            return reducedDamage;
        }

        public float GetStaminaReturn()
        {
            if (currentHouse != HouseType.Loyalty)
                return 0f;

            if (Time.time < lastLoyaltyTime + loyaltyCooldown)
                return 0f;

            lastLoyaltyTime = Time.time;

            return staminaReturnAmount;
        }
    }
}