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


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<PlayerMovement>();
    }


    void Update()
    {
        if (Input.GetButtonDown("Fire3") && player.xAxis != 0)
        {
            dashOn = true;
            currentDashTime = dashTimeReset;
            //rb.velocity = Vector2.zero;
            direction = player.xAxis;

            
        }
        if (dashOn)
        {
            //rb.AddForce(Vector2.right * dashSpeed, ForceMode2D.Impulse);
            rb.velocity = transform.right * direction * dashSpeed;
            currentDashTime -= Time.deltaTime;
            //Debug.Log(direction);
            Debug.Log(rb.velocity);

            if (currentDashTime <= 0)
            {
                dashOn = false;
            }
        }
    }
}
