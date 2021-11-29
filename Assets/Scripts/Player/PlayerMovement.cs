using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float pressJumpPower;
    public float minJumpPower;

    public float jumpTimer;
    public float jumpTimerValue;

    public float xAxis;

    public int jumpsLeft;
    public GameObject jumpFlames;

    Rigidbody2D player;
    DashController dash;
    //JumpsCounter jumpsCounter;

    Vector2 movement = new Vector2();

    Vector3 offsetFlames;

    public int maxJumps;

    public bool isGrounded;

    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        dash = GetComponent<DashController>();
        //jumpsCounter = GetComponent<JumpsCounter>();

        jumpsLeft = maxJumps;
    }

    void Update()
    {
        Jump();
        Timers();
        JumpsLeftLimiter();
        Respawn();
        
        //jumpsCounter.CountingJumpsLeft();

        offsetFlames = transform.position;
        offsetFlames.y -= 0.2f;
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpsLeft > 0)
        {
            player.velocity = Vector2.up * minJumpPower;
            jumpTimer = jumpTimerValue;
            jumpsLeft--;
            Instantiate(jumpFlames, offsetFlames, Quaternion.identity);
        }
        if (Input.GetKey(KeyCode.Space) && jumpTimer > 0)
        {
            player.velocity = Vector2.up * minJumpPower;
        }
    }

    private void FixedUpdate()
    {
        xAxis = Input.GetAxisRaw("Horizontal");
        if (!dash.dashOn)
        {
            player.velocity = new Vector2(xAxis * speed, player.velocity.y);
        }
    }
    
    private void Timers()
    {
        if (jumpTimer > 0) jumpTimer -= Time.deltaTime;
    }
    private void Respawn()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            GameObject.FindGameObjectWithTag("Checkpoint").GetComponent<CheckpointSystem>().RespawnPlayer();
        }
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
        if (other.CompareTag("jumpFlames")) // && jumpTimer <= 0
        {
            Destroy(other.gameObject);
            jumpsLeft++;
        }
        if (other.gameObject.tag == "Water")
        {
            GameObject.FindGameObjectWithTag("Checkpoint").GetComponent<CheckpointSystem>().RespawnPlayer();
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
    
   

