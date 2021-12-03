using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashController : MonoBehaviour
{
    Rigidbody2D rb;
    PlayerMovement playerScript;

    public float dashSpeed;
    public float dashTimeReset;
    
    float currentDashTime;
    Vector2 playerMove;
    float direction;

    public bool dashOn;

    public float cooldownReset;
    float dashCooldown;

    float startGrav;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerScript = GetComponent<PlayerMovement>();


        startGrav = rb.gravityScale;
        dashCooldown = cooldownReset;
    }


    void Update()
    {
        dashCooldown -= Time.deltaTime;

        if (Input.GetButtonDown("Fire3") && playerScript.xAxis != 0 && dashCooldown <= 0)
        {
            if (playerScript.oneDashOnAir)
            {
                dashOn = true;
                playerScript.oneDashOnAir = true;

                currentDashTime = dashTimeReset;
                //rb.velocity = Vector2.zero;
                playerMove = new Vector2(playerScript.xAxis, playerScript.yAxis);
                playerMove.Normalize();
                rb.gravityScale = 0;
            }
        }

        if (dashOn)
        {
            playerScript.Detach();
            
            rb.velocity = (playerMove * dashSpeed);
            currentDashTime -= Time.deltaTime;

            if (currentDashTime <= 0)
            {
                dashOn = false;
                rb.gravityScale = startGrav;
                dashCooldown = cooldownReset;
            }
        }
    }
}
