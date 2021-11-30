using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashController : MonoBehaviour
{
    Rigidbody2D rb;
    PlayerMovement2 player;

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
        //player = GetComponent<PlayerMovement>();
        player = GetComponent<PlayerMovement2>();


        startGrav = rb.gravityScale;
        dashCooldown = cooldownReset;
    }


    void Update()
    {
        dashCooldown -= Time.deltaTime;

        if (Input.GetButtonDown("Fire3") && dashCooldown <= 0)//&& player.xAxis != 0
        {
            if (player.xAxis != 0 || player.yAxis != 0)
            {
                dashOn = true;
                currentDashTime = dashTimeReset;
                //rb.velocity = Vector2.zero;
                playerMove = new Vector2(player.xAxis, player.yAxis);
                playerMove.Normalize();
               // rb.gravityScale = 0;
            }
        }

        if (dashOn)
        {
            player.Detach();
            
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
