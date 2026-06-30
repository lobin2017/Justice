using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float moveSpeed = 5f;

    private PlayerInputActions inputActions;
    private Animator animator;
    private Rigidbody2D rb;

    private Vector2 moveInput;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
    }

    private void OnDisable()
    {
        inputActions.Player.Disable();
    }


    void Update()
    {
        moveInput = inputActions.Player.Move.ReadValue<Vector2>();

        bool isMoving = moveInput.sqrMagnitude > 0.01f;
        animator.SetBool("isMoving", isMoving);
        
    }
    private void FixedUpdate()
    {
        rb.linearVelocity = moveInput * moveSpeed;
    }
}