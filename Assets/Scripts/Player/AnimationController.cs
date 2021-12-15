using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public PlayerMovement playerScript;
    public Animator animator;
    public SpriteRenderer feetSpriteRenderer;
    public ParticleSystem jumpParticles;

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
            feetSpriteRenderer.flipX = true;
            animator.SetBool("IsWaiting", false);
            waitingAnimationTime = resetWaitingAnimTime;
        }
        else if (playerScript.xAxis > 0)
        {
            feetSpriteRenderer.flipX = false;
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

            // Create a method in PlayerMovement script for the initial jump and call that instead (the code repeats here)
            if (Input.GetKeyDown(KeyCode.Space) && playerScript.jumpsLeft > 0) jumpParticles.Play();
        }
        else if (playerScript.jumping)
        {
            animator.SetBool("IsJumping", true);

            // Create a method in PlayerMovement script for the initial jump and call that instead (the code repeats here)
            if (Input.GetKeyDown(KeyCode.Space) && playerScript.jumpsLeft > 0) jumpParticles.Play();
        }
    }
}
