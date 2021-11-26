using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashController : MonoBehaviour
{
    Rigidbody2D rb;
    PlayerMovement player;

    public float dashSpeed;
    public float dashTimeReset;
    
    float currentDashTime;
    float direction;

    public bool dashOn;

    public float cooldownReset;
    float dashCooldown;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<PlayerMovement>();

        dashCooldown = cooldownReset;
    }


    void Update()
    {
        dashCooldown -= Time.deltaTime;

        if (Input.GetButtonDown("Fire3") && player.xAxis != 0 && dashCooldown <= 0)
        {
            dashOn = true;
            currentDashTime = dashTimeReset;
            //rb.velocity = Vector2.zero;
            direction = player.xAxis;
        }

        if (dashOn)
        {
            rb.velocity = transform.right * direction * dashSpeed;
            currentDashTime -= Time.deltaTime;

            if (currentDashTime <= 0)
            {
                dashOn = false;
                dashCooldown = cooldownReset;
            }
        }
    }
}
