using UnityEngine;

namespace Player
{
    public class PlayerInput : MonoBehaviour
    {
        public static PlayerInput Instance { get; private set; }

        public PlayerInputActions Actions { get; private set; }

        private void Awake()
        {
            Instance = this;
            Actions = new PlayerInputActions();
        }

        private void OnEnable()
        {
            Actions.Player.Enable();
        }

        private void OnDisable()
        {
            Actions.Player.Disable();
        }
    }
}