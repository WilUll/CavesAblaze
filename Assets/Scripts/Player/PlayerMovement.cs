using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float pressJumpPower;
    public float minJumpPower;

    public float dashTimer;
    public float jumpTimer;
    public float jumpTimerValue;

    float xAxis;

    bool dashOn = true;

    public int jumpsLeft;
    public GameObject jumpFlames;

    Rigidbody2D player;
    Vector2 movement = new Vector2();

    Vector3 offsetFlames;

    float dashSpeed;

    public int maxJumps;

    public bool isGrounded;

    void Start()
    {
        player = GetComponent<Rigidbody2D>();

        maxJumps = jumpsLeft;
    }

    void Update()
    {
        //float x = Input.GetAxis("Horizontal");

        Jump();
        //Dash();
        Timers();
        JumpsLeftLimiter();

        //movement.x = dashSpeed * x + speed * x;

        offsetFlames = transform.position;
        offsetFlames.y -= 0.2f;
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpsLeft > 0)
        {
            //player.AddForce(Vector2.up * minJumpPower, ForceMode2D.Impulse);
            player.velocity = Vector2.up * minJumpPower;
            jumpTimer = jumpTimerValue;
            jumpsLeft--;
            Instantiate(jumpFlames, offsetFlames, Quaternion.identity);
        }

        //if (Input.GetKey(KeyCode.Space) && jumpTimer > 0)
        //{
        //    //player.AddForce(Vector2.up * pressJumpPower, ForceMode2D.Impulse);
        //    player.velocity = Vector2.up * pressJumpPower;
        //}
    }

    private void FixedUpdate()
    {
        xAxis = Input.GetAxisRaw("Horizontal");
        player.velocity = new Vector2(xAxis * speed, player.velocity.y);
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        if (other.CompareTag("jumpFlames") && jumpTimer <= 0)
        {
            Destroy(other.gameObject);
            jumpsLeft++;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
    
   

