using UnityEngine;

namespace BossBattle
{
    [System.Serializable]
    public struct HouseProfile
    {
        public string houseName;     
        public string leaderName;    
        public string modifierPower; 
    }

    public class HouseBossController : MonoBehaviour
    {
        [Header("--- 가주 프로필 정의 ---")]
        public HouseBossType bossType;
        public HouseProfile profile;

        [Header("--- 보스 기본 능력치 ---")]
        public float maxHP = 2000f;
        public float currentHP;
        public float moveSpeed = 5f;

        [Header("--- 페이즈 상태 제어 ---")]
        public bool isBerserk = false;           
        public bool isCurrentlyInBattle = false;  

        private int loyaltyStack = 0;
        private float lastHitTime = 0f;
        private int hitGaugeForHand = 0;
        private bool isDoingRitual = false;
        private float ritualDamageAccumulated = 0f;

        public void InitializeBoss()
        {
            currentHP = maxHP;
            SetupProfileByEnum();
        }

        private void SetupProfileByEnum()
        {
            switch (bossType)
            {
                case HouseBossType.Valten_Aster:
                    profile.houseName = "House Valten"; profile.leaderName = "Aster"; profile.modifierPower = "집행";
                    break;
                case HouseBossType.Serane_Caelis:
                    profile.houseName = "House Serane"; profile.leaderName = "Caelis"; profile.modifierPower = "순명";
                    break;
                case HouseBossType.Ordel_Lucien:
                    profile.houseName = "House Ordel"; profile.leaderName = "Lucien"; profile.modifierPower = "규율";
                    break;
                case HouseBossType.Verdan_Serin:
                    profile.houseName = "House Verdan"; profile.leaderName = "Serin"; profile.modifierPower = "충의";
                    break;
            }
        }

        private void Update()
        {
            if (currentHP <= 0) return;

            if (bossType == HouseBossType.Verdan_Serin && loyaltyStack > 0)
            {
                if (Time.time - lastHitTime >= 2f) loyaltyStack = 0;
            }
        }

        public void ActivateBerserkMode()
        {
            isBerserk = true;
            isCurrentlyInBattle = true; 

            switch (bossType)
            {
                case HouseBossType.Valten_Aster:
                    break;
                case HouseBossType.Serane_Caelis:
                    InvokeRepeating("StartObedienceRitual", 5f, 15f);
                    break;
                case HouseBossType.Ordel_Lucien: 
                    InvokeRepeating("OrderRegenTick", 1f, 1f);
                    break;
                case HouseBossType.Verdan_Serin: 
                    break;
            }
        }
        public void TakeDamage(float incomingDamage)
        {
            if (currentHP <= 0) return;
            float finalDamage = incomingDamage;

            if (bossType == HouseBossType.Serane_Caelis && isDoingRitual)
            {
                ritualDamageAccumulated += incomingDamage;
                if (ritualDamageAccumulated >= 300f)
                {
                    StopObedienceRitual();
                }
            }

            if (bossType == HouseBossType.Verdan_Serin)
            {
                if (isBerserk)
                {
                    finalDamage *= 0.6f;
                    hitGaugeForHand++;
                    if (hitGaugeForHand >= 4) { SpawnDarkHand(); hitGaugeForHand = 0; }
                }
                else if (isCurrentlyInBattle)
                {
                    loyaltyStack = Mathf.Min(loyaltyStack + 1, 5);
                    lastHitTime = Time.time;
                    finalDamage *= (1f - (loyaltyStack * 0.05f));
                }
            }

            currentHP -= finalDamage;

            if (currentHP <= 0)
            {
                HouseWarManager.Instance.ReportBossDeath(this);
                CancelInvoke();
                Destroy(gameObject);
            }
        }

        public void OnAttackSuccess()
        {
            if (bossType == HouseBossType.Valten_Aster && isBerserk)
            {
                currentHP -= (currentHP * 0.02f);
            }
        }

        public void OnUseSkill()
        {
            if (bossType == HouseBossType.Ordel_Lucien && isBerserk)
            {
                currentHP -= (currentHP * 0.03f);
            }
        }

        private void OrderRegenTick()
        {
            if (currentHP > 0) currentHP = Mathf.Min(currentHP + (maxHP * 0.005f), maxHP);
        }

        private void StartObedienceRitual()
        {
            isDoingRitual = true;
            ritualDamageAccumulated = 0f;
            Debug.Log($"<color=purple>[의식 경고]</color> {profile.leaderName}의 강탈 의식이 시작됩니다! 타격하여 저지하세요!");
            Invoke("CompleteObedienceRitual", 4f);
        }

        private void CompleteObedienceRitual()
        {
            if (!isDoingRitual) return;
            isDoingRitual = false;
            moveSpeed += 2f;
            Debug.Log($"<color=red>[의식 성공]</color> {profile.leaderName}이 플레이어의 스탯을 흡수했습니다.");
        }

        private void StopObedienceRitual()
        {
            isDoingRitual = false;
            CancelInvoke("CompleteObedienceRitual");
            Debug.Log($"<color=cyan>[의식 저지]</color> 플레이어의 맹공으로 {profile.leaderName}의 의식이 파쇄되었습니다.");
        }

        private void SpawnDarkHand()
        {
            Debug.Log($"<color=gray>[검은 손]</color> {profile.leaderName}의 후방에서 플레이어를 옭아맬 검은 손이 사출됩니다.");
        }

        public void SetBattleEngagement(bool engage, Vector3 targetPosition)
        {
            isCurrentlyInBattle = engage;

            if (engage)
            {
                gameObject.SetActive(true);
                transform.position = targetPosition;

                var agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
                if (agent != null) agent.isStopped = false;
            }
            else
            {
                var agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
                if (agent != null) agent.isStopped = true;

                transform.position = targetPosition;
                gameObject.SetActive(false); 
            }
        }
    }
}