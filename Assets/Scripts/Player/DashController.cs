using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashController : MonoBehaviour
{
    Rigidbody2D rb;
    PlayerMovement playerScript;
    ShakeCamera cameraShake;
    Camera camera;

    public float dashSpeed;
    public float dashTimeReset;
    
    float currentDashTime;
    Vector2 playerMove;
    float direction;

    public bool dashOn, wallCollisionDashOn;

    public float cooldownReset, wallBounceCooldownReset = 0.2f;

    float dashCooldown, wallDashCooldown;

    float amountShake = 0.1f, lenghtShake = 0.3f;

    float startGrav;
    public float lastDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerScript = GetComponent<PlayerMovement>();

        camera = Camera.main;
        cameraShake = camera.GetComponent<ShakeCamera>(); 

        startGrav = rb.gravityScale;
        ResetDashCooldown();
    }


    void Update()
    {
        CheckLastXAxisDirection();

        RunDashTimer();
        RunWallCooldownTimer();
        HandleDashInputConditions();

        Debug.Log(dashOn);
        HandleDashMovement();
        Bounce();
    }


    private void CheckLastXAxisDirection()
    {
        if (playerScript.xAxis != 0)
        {
            lastDirection = playerScript.xAxis;
        }
    }

    private void RunDashTimer()
    {
        dashCooldown -= Time.deltaTime;
    }
    private void RunWallCooldownTimer()
    {
        wallDashCooldown -= Time.deltaTime;
    }
    private void HandleDashInputConditions()
    {
        if (Input.GetButtonDown("Fire3") && dashCooldown <= 0)
        {
            dashOn = true;
            playerScript.oneDashOnAir = true;

            currentDashTime = dashTimeReset;
            playerMove = new Vector2(lastDirection, 0);
            playerMove.Normalize();
            rb.gravityScale = 0;
        }
    }
    private void HandleDashMovement()
    {
        if (dashOn && !wallCollisionDashOn)
        {
            DashDetachOfRope();

            SetDashSpeed();
            RunDashTimeDuration();
            ResetDashConditions();
        }
    }
    private void Bounce()
    {
        if(wallCollisionDashOn)
        {
            MovementBounce();
            CheckDashWallTimer();
        }
    }

    private void DashDetachOfRope()
    {
        if (playerScript.isAttached && currentDashTime >= dashTimeReset - 0.05)
        {
            playerScript.Detach();
        }
    }
    private void SetDashSpeed()
    {
        rb.velocity = (playerMove * dashSpeed);
    }
    private void RunDashTimeDuration()
    {
        currentDashTime -= Time.deltaTime;
    }
    private void ResetDashConditions()
    {
        if (currentDashTime <= 0)
        {
            dashOn = false;
            RestartGravity();
            ResetDashCooldown();
        }
    }
    private void ResetDashCooldown()
    {
        dashCooldown = cooldownReset;
    }
    private void RestartGravity()
    {
        rb.gravityScale = startGrav;
    }
    
    private void ActivateConditions()
    {
        wallCollisionDashOn = true;
        //dashOn = true so we cannot move our player meanwhile.
        dashOn = true;
        wallDashCooldown = wallBounceCooldownReset;
    }
    private void MovementBounce()
    {
        rb.velocity = (playerMove * dashSpeed * -1);
    }
    private void CheckDashWallTimer()
    {
        if (wallDashCooldown <= 0)
        {
            ResetConditions();
            RestartGravity();
        }
    }
    private void ResetConditions()
    {
        dashOn = false;
        wallCollisionDashOn = false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("DashWall") && dashOn)
        {
            ActivateConditions();
            ShakeCameraDashWall();
        }
    }


    private void ShakeCameraDashWall()
    {
        cameraShake.Shake(amountShake, lenghtShake);
    }
}
