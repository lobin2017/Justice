using Boss;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    public static BossManager Instance { get; private set; }

    [Header("Current Progress")]
    [SerializeField] private int currentStage = 1;
    [SerializeField] private int currentRoom = 1;

    public int CurrentStage => currentStage;
    public int CurrentRoom => currentRoom;

    public BossHealth TargetBoss { get; private set; }

    [Header("UI Reference")]
    [SerializeField] private GameObject rewardSelectionUI;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void RegisterBoss(BossHealth boss)
    {
        TargetBoss = boss;
    }

    public void OnBossKilled(BossController killedBoss)
    {
        if (killedBoss == null) return;

        Debug.Log($"보스 {killedBoss.gameObject.name} 처치 완료.");

        TargetBoss = null;

        if (currentRoom < 3)
        {
            currentRoom++;
            Debug.Log($"다음 방으로 이동합니다. (현재 방: {currentRoom}/3)");
        }
        else
        {
            OpenRewardUI();
        }
    }

    private void OpenRewardUI()
    {
        if (rewardSelectionUI != null)
        {
            rewardSelectionUI.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void SelectReward(string rewardType)
    {
        Debug.Log($"플레이어가 [{rewardType}] 힘을 선택했습니다.");
        Time.timeScale = 1f;

        if (rewardSelectionUI != null)
        {
            rewardSelectionUI.SetActive(false);
        }

        currentStage++;
        currentRoom = 1;
    }
}