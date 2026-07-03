using UnityEngine;

namespace Lobby
{
    // Justice님이 직접 구상하신 4대 가문의 중의적 수식어 키워드
    public enum HouseType
    {
        None,
        Blindness,  // 맹목 
        Obedience,  // 순명 
        Order,      // 규율 
        Loyalty     // 충의 
    }

    public class PlayerStatus : MonoBehaviour
    {
        public static PlayerStatus Instance { get; private set; }

        [Header("--- 가주 격파 후 각인된 가문의 힘 ---")]
        public HouseType currentHouse = HouseType.None;

        [Header("--- [맹목] 버프 데이터 ---")]
        private float lastBlindnessTime = -100f;
        public float blindnessCooldown = 60f; 

        [Header("--- [충의] 버프 데이터 ---")]
        private float lastLoyaltyTime = -100f;
        public float loyaltyCooldown = 4f;
        public float staminaReturnAmount = 15f;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else Destroy(gameObject);
        }

        public void InscribeHouseSign(HouseType newHouse)
        {
            currentHouse = newHouse;
            Debug.Log($"<color=green>[정의의 재해석]</color> {currentHouse} 가문의 힘을 용사의 신념으로 흡수했습니다.");
        }

        public float CalculateMitigatedDamage(float incomingDamage)
        {
            if (currentHouse != HouseType.Blindness) return incomingDamage;

            if (Time.time >= lastBlindnessTime + blindnessCooldown)
            {
                float reducedDamage = incomingDamage * 0.7f; 
                lastBlindnessTime = Time.time;
                Debug.Log($"<color=cyan>[맹목의 수호 발동]</color> 대미지 30% 경감 ({incomingDamage} -> {reducedDamage})");
                return reducedDamage;
            }
            return incomingDamage;
        }

        public float GetStaminaReturn()
        {
            if (currentHouse != HouseType.Loyalty) return 0f;

            if (Time.time >= lastLoyaltyTime + loyaltyCooldown)
            {
                lastLoyaltyTime = Time.time;
                return staminaReturnAmount;
            }
            return 0f;
        }
    }
}