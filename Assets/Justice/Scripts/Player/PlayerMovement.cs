using Player;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerAnimation))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;

    private Rigidbody2D rb;
    private PlayerAnimation animationController;
    private PlayerInputActions inputActions;

    private Vector2 moveInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animationController = GetComponent<PlayerAnimation>();

        inputActions = PlayerInput.Instance.Actions;

        if (rb == null)
            Debug.LogError($"{name} : Rigidbody2D가 없습니다.");

        if (animationController == null)
            Debug.LogError($"{name} : PlayerAnimationController가 없습니다.");
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
        ReadInput();
        UpdateAnimation();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void ReadInput()
    {
        moveInput = inputActions.Player.Move.ReadValue<Vector2>();
    }

    private void UpdateAnimation()
    {
        if (animationController == null)
            return;

        PlayerState state =
            moveInput.sqrMagnitude > 0.01f
            ? PlayerState.Walk
            : PlayerState.Idle;

        animationController.UpdateAnimation(moveInput, state);
    }

    private void Move()
    {
        if (rb == null)
            return;

        rb.linearVelocity = moveInput * moveSpeed;
    }
}