using Player;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(PlayerAnimationController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private Rigidbody2D rb;
    private PlayerAnimationController animationController;
    private PlayerInputActions inputActions;

    private Vector2 moveInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animationController = GetComponent<PlayerAnimationController>();

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
        moveInput = inputActions.Player.Move.ReadValue<Vector2>();

        PlayerState state =
            moveInput.sqrMagnitude > 0.01f
            ? PlayerState.Walk
            : PlayerState.Idle;

        animationController.UpdateAnimation(moveInput, state);
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = moveInput * moveSpeed;
    }
}