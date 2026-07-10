using UnityEngine;
using System.Collections.Generic;

namespace BossBattle
{
    public enum HouseBossType { Valten_Aster, Serane_Caelis, Ordel_Lucien, Verdan_Serin }
    public enum BattlePhase { Phase_1, Phase_2 }

    public class HouseWarManager : MonoBehaviour
    {
        public static HouseWarManager Instance { get; private set; }

        [Header("--- 전장 상태 ---")]
        public BattlePhase currentPhase = BattlePhase.Phase_1;

        public List<HouseBossController> allBosses = new List<HouseBossController>();

        public List<HouseBossController> activeBosses = new List<HouseBossController>();

        private List<HouseBossController> waitingBosses = new List<HouseBossController>();

        [Header("--- 1단계 태그 시스템 설정 ---")]
        public float tagInterval = 20f;
        private float tagTimer = 0f;
        private int deadBossCount = 0;

        [Header("--- 스폰 및 대기 위치 (선택 사항) ---")]
        public Transform battleZoneCenter; 
        public Transform waitZoneLeft;     
        public Transform waitZoneRight;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        private void Start()
        {
            foreach (var boss in FindObjectsOfType<HouseBossController>())
            {
                if (!allBosses.Contains(boss)) allBosses.Add(boss);
                boss.InitializeBoss();
            }

            if (allBosses.Count < 4)
            {
                Debug.LogError("[가주전] 씬에 가주 보스가 4명 미만입니다! 프리팹 배치를 확인하세요.");
                return;
            }

            SetupInitialTagMatch();
        }

        private void Update()
        {
            if (currentPhase == BattlePhase.Phase_2) return;

            tagTimer += Time.deltaTime;
            if (tagTimer >= tagInterval)
            {
                tagTimer = 0f;
                ExecuteTagExchange(); 
            }
        }

        private void SetupInitialTagMatch()
        {
            List<HouseBossController> shuffleList = new List<HouseBossController>(allBosses);

            for (int i = 0; i < shuffleList.Count; i++)
            {
                int rnd = Random.Range(i, shuffleList.Count);
                var temp = shuffleList[i];
                shuffleList[i] = shuffleList[rnd];
                shuffleList[rnd] = temp;
            }

            for (int i = 0; i < shuffleList.Count; i++)
            {
                if (i < 2)
                {
                    activeBosses.Add(shuffleList[i]);
                    shuffleList[i].SetBattleEngagement(true, battleZoneCenter != null ? battleZoneCenter.position : Vector3.zero);
                }
                else
                {
                    waitingBosses.Add(shuffleList[i]);
                    Vector3 waitPos = (i == 2 && waitZoneLeft != null) ? waitZoneLeft.position : (waitZoneRight != null ? waitZoneRight.position : Vector3.left * 10f);
                    shuffleList[i].SetBattleEngagement(false, waitPos);
                }
            }

            Debug.Log($"[1페이즈 시작] 선발대: {activeBosses[0].Profile.leaderName}, {activeBosses[1].Profile.leaderName} / 대기조: {waitingBosses[0].Profile.leaderName}, {waitingBosses[1].Profile.leaderName}");
        }

        private void ExecuteTagExchange()
        {
            if (waitingBosses.Count == 0 || activeBosses.Count == 0) return;

            int activeIdx = Random.Range(0, activeBosses.Count);
            HouseBossController bossToOut = activeBosses[activeIdx];

            int waitingIdx = Random.Range(0, waitingBosses.Count);
            HouseBossController bossToIn = waitingBosses[waitingIdx];

            activeBosses.Remove(bossToOut);
            waitingBosses.Remove(bossToIn);

            activeBosses.Add(bossToIn);
            waitingBosses.Add(bossToOut);

            Vector3 centerPos = battleZoneCenter != null ? battleZoneCenter.position : Vector3.zero;
            Vector3 waitPos = waitZoneLeft != null ? waitZoneLeft.position : Vector3.left * 10f;

            bossToOut.SetBattleEngagement(false, waitPos); 
            bossToIn.SetBattleEngagement(true, centerPos);  

            //Debug.Log($"<color=yellow>[가주 교대 발생]</color>  {bossToOut.profile.leaderName} 퇴장 ➡️ {bossToIn.profile.leaderName} 입장!");
        }

        public void ReportBossDeath(HouseBossController deadBoss)
        {
            deadBossCount++;

            if (activeBosses.Contains(deadBoss)) activeBosses.Remove(deadBoss);
            if (waitingBosses.Contains(deadBoss)) waitingBosses.Remove(deadBoss);
            if (allBosses.Contains(deadBoss)) allBosses.Remove(deadBoss);

            Debug.Log($"<b>[{deadBoss.Profile.houseName}] {deadBoss.Profile.leaderName}</b>이(가) 쓰러졌습니다. (현재 처치: {deadBossCount}/4)");

            if (deadBossCount == 2 && currentPhase == BattlePhase.Phase_1)
            {
                TriggerPhase2();
            }
            else if (currentPhase == BattlePhase.Phase_1 && waitingBosses.Count > 0)
            {
                HouseBossController urgentIn = waitingBosses[0];
                waitingBosses.Remove(urgentIn);
                activeBosses.Add(urgentIn);
                urgentIn.SetBattleEngagement(true, battleZoneCenter != null ? battleZoneCenter.position : Vector3.zero);
                //Debug.Log($"[긴급 참전] 빈자리를 메우기 위해 {urgentIn.profile.leaderName} 가주가 전장에 난입합니다.");
            }
        }

        private void TriggerPhase2()
        {
            currentPhase = BattlePhase.Phase_2;

            Debug.Log("<color=red><b>국왕: \"선고하노라.\"</b></color>");
            Debug.Log("<color=orange>[선고의 힘]</color> 남은 두 가주의 봉인이 해제되며 전원 폭주 상태로 상시 참전합니다!");

            foreach (var remainingBoss in allBosses)
            {
                if (!activeBosses.Contains(remainingBoss)) activeBosses.Add(remainingBoss);

                remainingBoss.SetBattleEngagement(true, battleZoneCenter != null ? battleZoneCenter.position : Vector3.zero);
                remainingBoss.ActivateBerserkMode();
            }

            waitingBosses.Clear();
        }
    }
}