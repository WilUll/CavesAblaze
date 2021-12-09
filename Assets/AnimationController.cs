using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public PlayerMovement playerScript;
    public Animator animator;
    public SpriteRenderer playerSpriteRenderer;

    float waitingAnimationTime;
    public float resetWaitingAnimTime;

    void Update()
    {
        animator.SetFloat("Speed", Mathf.Abs(playerScript.xAxis));

        if (playerScript.xAxis < 0)
        {
            playerSpriteRenderer.flipX = true;
            animator.SetBool("IsWaiting", false);
            waitingAnimationTime = resetWaitingAnimTime;
        }
        else if (playerScript.xAxis > 0)
        {
            playerSpriteRenderer.flipX = false;
            animator.SetBool("IsWaiting", false);
            waitingAnimationTime = resetWaitingAnimTime;
        }
        else if (playerScript.xAxis == 0)
        {
            waitingAnimationTime -= Time.deltaTime;
            if (waitingAnimationTime <= 0) WaitingAnimation();
        }

        if (playerScript.jumping)
        {
            animator.SetBool("IsJumping", true);
        }

        JumpAnimation();
    }
    private void WaitingAnimation()
    {
        animator.SetBool("IsWaiting", true);
    }
    private void JumpAnimation()
    {
        if (playerScript.isGrounded)
        {
            animator.SetBool("IsJumping", false);
        }
    }
}
