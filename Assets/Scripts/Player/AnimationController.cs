using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public PlayerMovement playerScript;
   // public Animator animatorFeet;
    public Animator animatorBody;
    //public SpriteRenderer feetSpriteRenderer;
    public SpriteRenderer eyesSpriteRenderer;
    public ParticleSystem jumpParticles;
    public DashController dashScript;

    float waitingAnimationTime;
    public float resetWaitingAnimTime;

    void Update()
    {
        //Set the float that handles Run and waiting animations
        SetAnimatorFloatSpeed();

        DashAninimation();
        DefineRunAndWaitingAnimations();
        DefineDashAnimation();
        JumpAnimation();
        FallAnimation();
    }

    private void FallAnimation()
    {
        animatorBody.SetFloat("YVelocity", playerScript.playerRB.velocity.y);
    }

    private void SetAnimatorFloatSpeed()
    {
        animatorBody.SetFloat("Speed", Mathf.Abs(playerScript.xAxis));
    }
    private void DefineRunAndWaitingAnimations()
    {
        //Move Animation
        if (playerScript.xAxis < 0)
        {
            eyesSpriteRenderer.flipX = false;
        }
        if (playerScript.xAxis > 0)
        {
            eyesSpriteRenderer.flipX = true;
        }
        ////Waiting Animation
        //else if (playerScript.xAxis == 0)
        //{
        //    waitingAnimationTime -= Time.deltaTime;
        //    if (waitingAnimationTime <= 0) WaitingAnimation();
        //}
    }
    private void DefineDashAnimation()
    {
        if (dashScript.lastDirection > 0 && dashScript.dashOn)
        {
            eyesSpriteRenderer.flipX = true;
        }
        else if (dashScript.lastDirection < 0 && dashScript.dashOn)
        {
            eyesSpriteRenderer.flipX = false;
        }
    }


    private void WaitingAnimation()
    {
        //animatorFeet.SetBool("IsWaiting", true);
    }
    private void JumpAnimation()
    {
        if (playerScript.isGrounded)
        {
            // Create a method in PlayerMovement script for the initial jump and call that instead (the code repeats here)
            if (Input.GetButtonDown("Jump") && playerScript.currentJumpsLeft > 0)
            {
                jumpParticles.Play();
            }
            
            //animatorFeet.SetBool("IsJumping", false);
        }
        else if (playerScript.jumping)
        {
            // Create a method in PlayerMovement script for the initial jump and call that instead (the code repeats here)
            if (Input.GetButtonDown("Jump") && playerScript.currentJumpsLeft > 0)
            {
                jumpParticles.Play();
            }
            //animatorFeet.SetBool("IsJumping", true);
        }

        if (playerScript.jumping) animatorBody.SetBool("IsJumping", true);
        else animatorBody.SetBool("IsJumping", false);
    }
    private void DashAninimation()
    {
        if (dashScript.dashOn)
        {
            animatorBody.SetBool("IsDashing", true);
        }
        else
        {
            animatorBody.SetBool("IsDashing", false);
        }
    }
}
