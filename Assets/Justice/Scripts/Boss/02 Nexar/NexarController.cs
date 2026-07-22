using UnityEngine;
using GameManager;

namespace Nexar
{
    public class NexarController : MonoBehaviour
    {
        [SerializeField]
        private GameObject warningPrefab;

        private CurseManager curseManager;

        private void Awake()
        {
            curseManager = GetComponent<CurseManager>();
        }

        public void UseFaithCurse()
        {
            curseManager.ApplyCurse(CurseType.Faith);
        }
        public void UseEnduranceCurse()
        {
            curseManager.ApplyCurse(CurseType.Endurance);
        }
        public void UseDevotionCurse()
        {
            curseManager.ApplyCurse(CurseType.Devotion);
        }
        public void UseDutyCurse()
        {
            curseManager.ApplyCurse(CurseType.Duty);
        }
    }
}