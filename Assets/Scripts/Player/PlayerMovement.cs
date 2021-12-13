using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject jumpFlames;
    
    Rigidbody2D playerRB;
    DashController dash;
    GameObject ropeObj;

    public float speed;
    public float pressJumpPower;
    public float minJumpPower;

    public float jumpTimer;
    public float jumpTimerValue;

    public float xAxis, yAxis;

    public int jumpsLeft;

    public bool oneDashOnAir;

    //JumpsCounter jumpsCounter;

    Vector2 movement = new Vector2();

    Vector3 offsetFlames;

    public int maxJumps;

    public bool isGrounded, playerDead, respawned, refilled, isAttached, jumping;

    bool varSet = false;

   
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        dash = GetComponent<DashController>();

        jumpsLeft = maxJumps;

        playerDead = false;
    }

    void Update()
    {
        xAxis = Input.GetAxisRaw("Horizontal");
        yAxis = Input.GetAxisRaw("Vertical");
        Timers();
        JumpsLeftLimiter();
        Jump();

        if (respawned) respawned = false;
        Respawn();

        offsetFlames = transform.position;
        offsetFlames.y -= 0.2f;

        if (jumpTimer <= 0 || isGrounded) jumping = false;
        
    }


    private void FixedUpdate()
    {
        if (!dash.dashOn && !isAttached)
        {
            playerRB.velocity = new Vector2(xAxis * speed, playerRB.velocity.y);
        }

        if (isAttached)
        {
            playerRB.transform.position = ropeObj.transform.position;
            playerRB.gravityScale = 0;
            varSet = true;
        }
        if (isAttached && varSet)
        {
            ropeObj.GetComponent<Rigidbody2D>().AddForce(new Vector2(xAxis * speed, 0));
        }
    }
    private void Jump()
    {
        if (!isAttached)
        {
            if (Input.GetKeyDown(KeyCode.Space) && jumpsLeft > 0)
            {
                playerRB.velocity = Vector2.up * minJumpPower;
                jumpTimer = jumpTimerValue;
                jumpsLeft--;
                oneDashOnAir = true;
                jumping = true;

                Instantiate(jumpFlames, offsetFlames, Quaternion.identity);
            }
            if (Input.GetKey(KeyCode.Space) && jumpTimer > 0)
            {
                playerRB.velocity = Vector2.up * pressJumpPower;
                jumping = true;
            }
        }
        else if (isAttached)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                playerRB.velocity = Vector2.up * minJumpPower;
                jumpTimer = jumpTimerValue;
            }
            if (Input.GetKey(KeyCode.Space) && jumpTimer > 0)
            {
                playerRB.velocity = Vector2.up * pressJumpPower;
                Detach();
            }
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
            respawned = true;
        }
    }
    private void JumpsLeftLimiter()
    {
        if (jumpsLeft > maxJumps) jumpsLeft = maxJumps;
    }

    public void Attach(Rigidbody2D ropeBone)
    {
        ropeObj = ropeBone.gameObject;
        isAttached = true;
        gameObject.transform.parent = ropeBone.transform;
    }

    //Detaches player from the rope and enables rb
    public void Detach()
    {
        gameObject.transform.parent = null;
        StartCoroutine(varTimer());
        IEnumerator varTimer()
        {
            varSet = false;
            isAttached = false;
            yield return new WaitForSeconds(0.2f);
            playerRB.gravityScale = 4;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Rope attach
        if (!isAttached)
        {
            if (other.gameObject.tag=="Rope")
            {
                Attach(other.gameObject.GetComponent<Rigidbody2D>());
            }
        }

        if (other.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        //Destroys jumpflames
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //If water, respawn player
        if (collision.gameObject.tag == "Water")
        {
            GameObject.FindGameObjectWithTag("Checkpoint").GetComponent<CheckpointSystem>().RespawnPlayer();

            playerDead = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Water")
        {
            playerDead = false;
        }
    }


}



