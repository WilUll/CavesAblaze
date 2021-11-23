using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5;
    public float jumpPower = 10;

    public float dashTimer;
    public float jumpTimer;

    bool dashOn = true;

    public int jumpsLeft;
    public GameObject jumpFlames;

    Rigidbody2D player;
    Vector2 movement = new Vector2();

    Vector3 offsetFlames;

    float dashSpeed;

    public int maxJumps;

    void Start()
    {
        player = GetComponent<Rigidbody2D>();

        maxJumps = jumpsLeft;
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");

        Jump();
        Dash();
        Timers();
        JumpsLeftLimiter();

        movement.x = dashSpeed * x + speed * x;

        offsetFlames = transform.position;
        offsetFlames.y -= 0.2f;
    }

    private void Jump()
    {
        if(Input.GetButtonDown("Jump") && jumpsLeft > 0)
        {
            jumpTimer = 0.2f;
            jumpsLeft--;
            Instantiate(jumpFlames, offsetFlames, Quaternion.identity);
        }

        if (Input.GetButton("Jump") && jumpsLeft > 0 && jumpTimer > 0)
        {
            player.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
    }

    private void FixedUpdate()
    {
        movement.y = player.velocity.y;
        player.velocity = movement;
    }

    private void Dash()
    {
        if (Input.GetButtonDown("Fire3") && dashOn)
        {
            dashSpeed = 5;
            dashOn = false;
            dashTimer = 0.5f;
        }
        else if (dashTimer <= 0)
            {
                dashSpeed = 0;
                dashOn = true;
            }
    }
    private void Timers()
    {
        if (dashTimer > 0) dashTimer -= Time.deltaTime;

        if (jumpTimer > 0) jumpTimer -= Time.deltaTime;
    }
    private void JumpsLeftLimiter()
    {
        if (jumpsLeft > maxJumps) jumpsLeft = maxJumps;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("jumpFlames") && jumpTimer <= 0)
        {
            Destroy(collision.gameObject);
            jumpsLeft++;
        }
    }
    }

