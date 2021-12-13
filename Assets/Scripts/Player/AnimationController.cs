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
        //Set the float that handles Run and waiting animations
        SetAnimatorFloatSpeed();

        DefineRunAndWaitingAnimations();
        JumpAnimation();
    }

    private void SetAnimatorFloatSpeed()
    {
        animator.SetFloat("Speed", Mathf.Abs(playerScript.xAxis));
    }
    private void DefineRunAndWaitingAnimations()
    {
        //Move Animation
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
        //Waiting Animation
        else if (playerScript.xAxis == 0)
        {
            waitingAnimationTime -= Time.deltaTime;
            if (waitingAnimationTime <= 0) WaitingAnimation();
        }
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
        else if (playerScript.jumping)
        {
            animator.SetBool("IsJumping", true);
        }
    }
}
