//using UnityEngine;

//namespace BossBattle
//{
//    [System.Serializable]
//    public struct HouseProfile
//    {
//        public string houseName;
//        public string leaderName;
//        public string modifierPower;
//    }

//    public class HouseBossController : MonoBehaviour
//    {
//        [Header("House Info")]
//        [SerializeField] private HouseBossType bossType;

//        [SerializeField] private HouseProfile profile;

//        public HouseBossType BossType => bossType;
//        public HouseProfile Profile => profile;

//        private void Awake()
//        {
//            InitializeProfile();
//        }

//        private void InitializeProfile()
//        {
//            switch (bossType)
//            {
//                case HouseBossType.Valten_Aster:
//                    profile.houseName = "House Valten";
//                    profile.leaderName = "Aster";
//                    profile.modifierPower = "집행";
//                    break;

//                case HouseBossType.Serane_Caelis:
//                    profile.houseName = "House Serane";
//                    profile.leaderName = "Caelis";
//                    profile.modifierPower = "순명";
//                    break;

//                case HouseBossType.Ordel_Lucien:
//                    profile.houseName = "House Ordel";
//                    profile.leaderName = "Lucien";
//                    profile.modifierPower = "규율";
//                    break;

//                case HouseBossType.Verdan_Serin:
//                    profile.houseName = "House Verdan";
//                    profile.leaderName = "Serin";
//                    profile.modifierPower = "충의";
//                    break;
//            }
//        }

//        public void OnBattleStart()
//        {
//            Debug.Log($"{profile.houseName} - {profile.leaderName} 전투 시작");
//        }

//        public void OnBattleEnd()
//        {
//            Debug.Log($"{profile.houseName} 전투 종료");
//        }

//        public void OnEnterPhase2()
//        {
//            switch (bossType)
//            {
//                case HouseBossType.Valten_Aster:
//                    Debug.Log("집행의 힘이 각성합니다.");
//                    break;

//                case HouseBossType.Serane_Caelis:
//                    Debug.Log("순명의 의식이 시작됩니다.");
//                    break;

//                case HouseBossType.Ordel_Lucien:
//                    Debug.Log("규율이 공간을 지배합니다.");
//                    break;

//                case HouseBossType.Verdan_Serin:
//                    Debug.Log("충의가 극한까지 발현됩니다.");
//                    break;
//            }
//        }
//    }
//}