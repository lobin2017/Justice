using UnityEngine;
using Boss;
using GameManager;


[System.Serializable]
public struct HouseProfile
{
    public string houseName;
    public string leaderName;
    public string modifierPower;
}

public class HouseBossController : MonoBehaviour
{
    [Header("Boss Data (ScriptableObject)")]
    [SerializeField] private BossData bossData;

    [Header("House Info")]
    [SerializeField] private BossType bossType;
    [SerializeField] private HouseProfile profile;

    public BossType BossType => bossType;
    public HouseProfile Profile => profile;
    public BossData BossData => bossData;

    private SpriteRenderer spriteRenderer;
    private Collider2D bodyCollider;
    private BossHealth bossHealth;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        bodyCollider = GetComponent<Collider2D>();
        bossHealth = GetComponent<BossHealth>();

        InitializeProfile();
    }

    private void InitializeProfile()
    {
        if (bossData != null)
        {
            profile.leaderName = bossData.bossName;
        }

        switch (bossType)
        {
            case BossType.Valten:
                profile.houseName = "House Valten";
                profile.modifierPower = "집행";
                break;
            case BossType.Serane:
                profile.houseName = "House Serane";
                profile.modifierPower = "순명";
                break;
            case BossType.Ordel:
                profile.houseName = "House Ordel";
                profile.modifierPower = "규율";
                break;
            case BossType.Verdan:
                profile.houseName = "House Verdan";
                profile.modifierPower = "충의";
                break;
        }
    }

    public void InitializeBoss()
    {
        if (bossData == null)
        {
            Debug.LogError($"[{gameObject.name}] BossData가 할당되지 않았습니다!");
            return;
        }

        if (bossHealth != null)
        {
            bossHealth.Initialize(bossData.maxHealth);
        }

        Debug.Log($"{profile.leaderName} 초기화 완료 (체력: {bossData.maxHealth})");
    }

    public void SetBattleEngagement(bool isActive, Vector3 targetPosition)
    {
        transform.position = targetPosition;

        if (spriteRenderer != null) spriteRenderer.enabled = isActive;
        if (bodyCollider != null) bodyCollider.enabled = isActive;

        if (TryGetComponent<BossController>(out var commonController))
        {
            commonController.enabled = isActive;
        }

        if (isActive) OnBattleStart();
        else OnBattleEnd();
    }

    public void OnBattleStart()
    {
        Debug.Log($"<color=cyan>[참전]</color> {profile.houseName} - {profile.leaderName} 전장 진입");
    }

    public void OnBattleEnd()
    {
        Debug.Log($"<color=gray>[대기]</color> {profile.houseName} - {profile.leaderName} 대기 구역 이동");
    }

    public void ActivateBerserkMode()
    {
        Debug.Log($"<color=red>[폭주 각성]</color> {profile.leaderName} 가주가 제한을 해제합니다.");
        OnEnterPhase2();
    }

    public void OnEnterPhase2()
    {
        switch (bossType)
        {
            case BossType.Valten:
                Debug.Log("집행의 힘이 각성합니다. (공격 시 고정 추가 피해)");
                break;
            case BossType.Serane:
                Debug.Log("순명의 의식이 시작됩니다. (주기적 플레이어 속박)");
                break;
            case BossType.Ordel:
                Debug.Log("규율이 공간을 지배합니다. (전장 영역 제한)");
                break;
            case BossType.Verdan:
                Debug.Log("충의가 극한까지 발현됩니다. (남은 가주에게 보호막 부여)");
                break;
        }
    }

    public void Die()
    {
        HouseWarManager.Instance.ReportBossDeath(this);
        gameObject.SetActive(false);
    }
}
