using UnityEngine;
using System.Collections.Generic;

namespace GameManager
{
    public enum CurseType
    {
        None,
        Faith,
        Endurance,
        Devotion,
        Duty
    }

    public class CurseManager : MonoBehaviour
    {
        private List<CurseType> activeCurses;

        private float curseDuration = 3f;
        private void Awake()
        {
            activeCurses = new List<CurseType>();
        }

        public void ApplyCurse(CurseType curse)
        {
            if (!activeCurses.Contains(curse))
            {
                activeCurses.Add(curse);
            }
        }

        public void RemoveCurse(CurseType curse)
        {
            activeCurses.Remove(curse);
        }

        public bool HasCurse(CurseType curse)
        {
            return activeCurses.Contains(curse);
        }
        public void ClearCurses()
        {
            activeCurses.Clear();
        }
    }
}