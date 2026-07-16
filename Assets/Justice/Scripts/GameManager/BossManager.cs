using Core;
using UnityEngine;

namespace GameManager
{
    public class BossManager : MonoBehaviour
    {
        public static BossManager Instance { get; private set; }

        [Header("Dungeon Progress")]
        [SerializeField] private int currentStage = 1;
        [SerializeField] private int currentRoom = 1;

        [Header("Reward UI")]
        [SerializeField] private GameObject rewardSelectionUI;

        public int CurrentStage => currentStage;
        public int CurrentRoom => currentRoom;

        public BossHealth TargetBoss { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        #region Boss

        public void RegisterBoss(BossHealth boss)
        {
            TargetBoss = boss;
        }

        public void ClearBoss()
        {
            TargetBoss = null;
        }

        public void OnBossKilled(BossController boss)
        {
            if (boss == null)
                return;

            Debug.Log($"{boss.name} 처치 완료");

            ClearBoss();

            if (currentRoom < 3)
            {
                currentRoom++;
                Debug.Log($"다음 방 ({currentRoom}/3)");
            }
            else
            {
                OpenRewardUI();
            }
        }

        #endregion

        #region Reward

        private void OpenRewardUI()
        {
            if (rewardSelectionUI == null)
                return;

            rewardSelectionUI.SetActive(true);
            Time.timeScale = 0f;
        }

        public void SelectReward(string rewardName)
        {
            Debug.Log($"선택한 보상 : {rewardName}");

            CloseRewardUI();

            currentStage++;
            currentRoom = 1;
        }

        private void CloseRewardUI()
        {
            Time.timeScale = 1f;

            if (rewardSelectionUI != null)
                rewardSelectionUI.SetActive(false);
        }

        #endregion
    }
}