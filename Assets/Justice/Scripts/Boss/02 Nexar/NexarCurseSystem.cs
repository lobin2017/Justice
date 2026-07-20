using UnityEngine;
using System.Collections.Generic;
using Player;

namespace Nexar
{
    public enum CurseType
    {
        None,
        Faith,
        Endurance,
        Devotion,
        Duty
    }

    public class NexarCurseSystem : MonoBehaviour
    {
        private List<CurseType> activeCurses;
        
        private void Awake()
        {
            activeCurses = new List<CurseType>();
        }

        public void ApplyCurse(CurseType curse)
        {
            activeCurses.Add(curse);
        }

        public void RemoveCurse(CurseType curse)
        {
            activeCurses.Remove(curse);
        }

        public bool HasCurse(CurseType curse)
        {
            return activeCurses.Contains(curse);
        }

    }
}