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
    public float dashTimeReset, pushBackTimeReset;

    float currentDashTime;
    Vector2 playerMove;

    public bool dashOn, wallCollisionDashOn;

    public float cooldownReset, wallBounceCooldownReset = 0.2f;

    public float dashCooldown, wallDashCooldown;

    float amountShake = 0.1f, lenghtShake = 0.3f;

    bool pushBack;

    float startGrav;
    public float lastDirection;

    void Start()
    {

        playerScript = GetComponent<PlayerMovement>();
        rb = playerScript.GetComponent<Rigidbody2D>();

        camera = Camera.main;
        //cameraShake = camera.GetComponent<ShakeCamera>(); 

        startGrav = rb.gravityScale;
        ResetDashCooldown();
    }


    void Update()
    {
        CheckLastXAxisDirection();

        RunDashTimer();
        RunWallCooldownTimer();
        HandleDashInputConditions();
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
        if (Input.GetButtonDown("Fire3") && dashCooldown <= 0 && !dashOn) 
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position + Vector3.right * lastDirection, Vector3.right * lastDirection, 0.25f);
            Debug.DrawRay(transform.position + Vector3.right * lastDirection, Vector3.right * lastDirection, Color.red, 10);
            if (hit.collider != null)
            {
                if (hit.collider == hit.collider.CompareTag("Ground"))
                {
                    Debug.Log("Ground");
                }
                else if (hit.collider == hit.collider.CompareTag("DashWall"))
                {
                    RaycastHit2D hit2 = Physics2D.Raycast(transform.position + Vector3.right * lastDirection, Vector3.right * lastDirection, 0.001f);
                    if (hit2.collider == hit.collider.CompareTag("DashWall"))
                    {
                        pushBack = true;
                        dashOn = true;
                        playerScript.oneDashOnAir = true;

                        currentDashTime = pushBackTimeReset;
                        playerMove = new Vector2(-lastDirection, 0);
                        playerMove.Normalize();
                        rb.gravityScale = 0;
                    }
                    else
                    {
                        pushBack = false;
                        dashOn = true;
                        playerScript.oneDashOnAir = true;

                        currentDashTime = dashTimeReset;
                        playerMove = new Vector2(lastDirection, 0);
                        playerMove.Normalize();
                        rb.gravityScale = 0;
                    }
                    
                }
                else
                {
                    pushBack = false;
                    dashOn = true;
                    playerScript.oneDashOnAir = true;

                    currentDashTime = dashTimeReset;
                    playerMove = new Vector2(lastDirection, 0);
                    playerMove.Normalize();
                    rb.gravityScale = 0;
                }
            }
            else
            {
                pushBack = false;
                dashOn = true;
                playerScript.oneDashOnAir = true;

                currentDashTime = dashTimeReset;
                playerMove = new Vector2(lastDirection, 0);
                playerMove.Normalize();
                rb.gravityScale = 0;
            }
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
        if (wallCollisionDashOn)
        {
            MovementBounce();
            CheckDashWallTimer();
        }
    }

    private void DashDetachOfRope()
    {
        if (playerScript.isAttached && currentDashTime >= dashTimeReset - 0.01)
        {
            playerScript.Detach();
        }
    }
    private void SetDashSpeed()
    {
        if (pushBack)
        {
            rb.velocity = (playerMove * dashSpeed / 4);
        }
        else
        {
            rb.velocity = (playerMove * dashSpeed);
        }
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
        if (other.gameObject.CompareTag("DashWall") && dashOn)
        {
            ActivateConditions();
            ShakeCameraDashWall();
        }
    }


    private void ShakeCameraDashWall()
    {
        //cameraShake.Shake(amountShake, lenghtShake);
    }
}
