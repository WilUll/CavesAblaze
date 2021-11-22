using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5;
    public float jumpPower = 5;

    public float dashTimer;

    bool dashOn = true;

    Rigidbody2D player;
    Vector2 movement = new Vector2();
    bool grounded;

    float dashSpeed;

    void Start()
    {
        player = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (dashTimer > 0) dashTimer -= Time.deltaTime;

        float x = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && grounded)
        {
            player.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }

        if (Input.GetButtonDown("Fire3") && dashOn)
        {
            dashSpeed = 5;
            dashOn = false;
            dashTimer = 0.5f;
        }
        else
        {
            if (dashTimer <= 0)
            {
                dashSpeed = 0;
                dashOn = true;
            }
        }

        movement.x = dashSpeed * x + speed * x;
    }

    private void FixedUpdate()
    {
        movement.y = player.velocity.y;
        player.velocity = movement;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        grounded = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        grounded = false;
    }
}
