using Boss;
using UnityEngine;

public class BossAnimation : MonoBehaviour
{
    private Animator animator;

    private readonly int isMovingHash = Animator.StringToHash("isMoving");
    private readonly int attackHash = Animator.StringToHash("attack");
    private readonly int hitHash = Animator.StringToHash("hit");               
    private readonly int deathHash = Animator.StringToHash("death");           
    private readonly int phaseTransitionHash = Animator.StringToHash("phaseTransition"); 

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void UpdateAnimation(BossState currentState, Vector3 directionToPlayer)
    {
        if (animator == null) return;

        bool isMoving = (currentState == BossState.Chase);
        animator.SetBool(isMovingHash, isMoving);

        if (directionToPlayer.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (directionToPlayer.x < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public void PlayAttackTrigger() => animator?.SetTrigger(attackHash);

    public void PlayHit()
    {
        animator?.SetTrigger(hitHash);
    }

    public void PlayDeath()
    {
        animator?.SetTrigger(deathHash);
    }

    public void PlayPhaseTransition()
    {
        animator?.SetTrigger(phaseTransitionHash);
    }
}