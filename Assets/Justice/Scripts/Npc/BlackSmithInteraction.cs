using UnityEngine;

public class BlackSmithInteraction : MonoBehaviour
{
    [SerializeField] private GameObject dialogueUI;

    private bool canInteract;
    private bool isOpen;

    private PlayerInputActions inputActions;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
    }

    private void OnDisable()
    {
        inputActions.Player.Disable();
    }

    private void Update()
    {
        if (!canInteract)
            return;

        if (inputActions.Player.Interact.WasPressedThisFrame())
        {
            Debug.Log("Interact");

            isOpen = !isOpen;

            if (dialogueUI != null)
            {
                dialogueUI.SetActive(isOpen);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"Enter : {other.name}");

        if (!other.CompareTag("Player"))
            return;

        canInteract = true;

        Debug.Log("플레이어 접근");
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log($"Exit : {other.name}");

        if (!other.CompareTag("Player"))
            return;

        canInteract = false;
        isOpen = false;

        if (dialogueUI != null)
            dialogueUI.SetActive(false);

        Debug.Log("플레이어 이탈");
    }
}