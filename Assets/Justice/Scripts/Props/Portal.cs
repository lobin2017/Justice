using UnityEngine;
using UnityEngine.SceneManagement;
using Player;

public class Portal : MonoBehaviour
{
    [SerializeField] private string sceneName = "Justice_BossRoom";

    private bool canEnter;

    private PlayerInputActions inputActions;

    private void Awake()
    {
        inputActions = PlayerInput.Instance.Actions;
    }
    private void Update()
    {
        if (!canEnter)
            return;

        if (inputActions.Player.Interact.WasPressedThisFrame())
        {
            EnterDungeon();
        }
    }

    private void EnterDungeon()
    {
        SceneManager.LoadScene(sceneName);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        canEnter = true;
        Debug.Log("E를 눌러 던전 입장");
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        canEnter = false;
    }
}