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
        private void CreateWarning()
        {
            Vector2 position = transform.position;

            Instantiate(
                warningPrefab,
                position,
                Quaternion.identity
            );
        }
        private void Start()
        {
            UseFaithCurse();
        }
    }
}