using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public PlayerMovement playerScript;
    public Animator animatorFeet;
    public Animator animatorBody;
    public SpriteRenderer feetSpriteRenderer;
    public SpriteRenderer bodySpriteRenderer;
    public ParticleSystem jumpParticles;
    public DashController dashScript;

    float waitingAnimationTime;
    public float resetWaitingAnimTime;

    void Update()
    {
        //Set the float that handles Run and waiting animations
        SetAnimatorFloatSpeed();

        DefineRunAndWaitingAnimations();
        DefineDashAnimation();
        JumpAnimation();
        DashAninimation();
    }



    private void SetAnimatorFloatSpeed()
    {
        animatorFeet.SetFloat("Speed", Mathf.Abs(playerScript.xAxis));
    }
    private void DefineRunAndWaitingAnimations()
    {
        //Move Animation
        if (playerScript.xAxis < 0)
        {
            feetSpriteRenderer.flipX = true;
            animatorFeet.SetBool("IsWaiting", false);
            waitingAnimationTime = resetWaitingAnimTime;
        }
        else if (playerScript.xAxis > 0)
        {
            feetSpriteRenderer.flipX = false;
            animatorFeet.SetBool("IsWaiting", false);
            waitingAnimationTime = resetWaitingAnimTime;
        }
        //Waiting Animation
        else if (playerScript.xAxis == 0)
        {
            waitingAnimationTime -= Time.deltaTime;
            if (waitingAnimationTime <= 0) WaitingAnimation();
        }
    }
    private void DefineDashAnimation()
    {
        if (dashScript.lastDirection < 0 && dashScript.dashOn)
        {
            bodySpriteRenderer.flipX = true;
        }
        else if (dashScript.lastDirection > 0)
        {
            bodySpriteRenderer.flipX = false;
        }
    }


    private void WaitingAnimation()
    {
        animatorFeet.SetBool("IsWaiting", true);
    }
    private void JumpAnimation()
    {
        if (playerScript.isGrounded)
        {
            animatorFeet.SetBool("IsJumping", false);

            // Create a method in PlayerMovement script for the initial jump and call that instead (the code repeats here)
            if (Input.GetButtonDown("Jump") && playerScript.jumpsLeft > 0) jumpParticles.Play();
        }
        else if (playerScript.jumping)
        {
            animatorFeet.SetBool("IsJumping", true);

            // Create a method in PlayerMovement script for the initial jump and call that instead (the code repeats here)
            if (Input.GetButtonDown("Jump") && playerScript.jumpsLeft > 0) jumpParticles.Play();
        }
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
