using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject jumpFlames;
    
    Rigidbody2D playerRB;
    DashController dash;
    GameObject ropeObj;

    public bool isGrounded, dead, respawned, refilled, 
                isAttached, jumping, oneDashOnAir;

    public float xAxis, yAxis, speed;
    public float holdJumpPower, minJumpPower, jumpTimerValue, jumpTimer, jumpsLeft;
    public int maxJumps;

   
    bool varSet = false;
    Vector3 offsetFlames;

    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        dash = GetComponent<DashController>();

        jumpsLeft = maxJumps;

        dead = false;
    }

    void Update()
    {
        xAxis = Input.GetAxisRaw("Horizontal");
        yAxis = Input.GetAxisRaw("Vertical");

        Jump();

        RunJumpTimer();
        LimitJumpsLeft();

        SetJumpFlamesSpawningOffset();

        RestartRespawnedBool();
        Respawn();

        CheckIfJumping();
    }


    private void FixedUpdate()
    {
        Run();
        SwingInRope();
    }


    private void Run()
    {
        if (!dash.dashOn && !isAttached)
        {
            playerRB.velocity = new Vector2(xAxis * speed, playerRB.velocity.y);
        }
    }
    private void Jump()
    {
        if (!isAttached)
        {
            if (Input.GetKeyDown(KeyCode.Space) && jumpsLeft > 0)
            {
                CalculateInitialJumpPower();
                ResetJumpTimer();
                SetTrueJumpConditions();
                jumpsLeft--;

                Instantiate(jumpFlames, offsetFlames, Quaternion.identity);
            }
            if (Input.GetKey(KeyCode.Space) && jumpTimer > 0)
            {
                CalculateHoldJumpPower();
                SetTrueJumpConditions();
            }
        }
        else if (isAttached)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                CalculateInitialJumpPower();
                ResetJumpTimer();
                SetTrueJumpConditions();
            }
            if (Input.GetKey(KeyCode.Space) && jumpTimer > 0)
            {
                CalculateHoldJumpPower();
                SetTrueJumpConditions();
                Detach();
            }
        }
    }
    private void SwingInRope()
    {
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


    private void RunJumpTimer()
    {
        if (jumpTimer >= 0) jumpTimer -= Time.deltaTime;
    }
    private void ResetJumpTimer()
    {
        jumpTimer = jumpTimerValue;
    }
    private void LimitJumpsLeft()
    {
        if (jumpsLeft > maxJumps) jumpsLeft = maxJumps;
    }


    private void CalculateInitialJumpPower()
    {
        playerRB.velocity = Vector2.up * minJumpPower;
    }
    private void CalculateHoldJumpPower()
    {
        playerRB.velocity = Vector2.up * holdJumpPower;
    }
    private void SetTrueJumpConditions()
    {
        //oneDashOnAir = true;
        jumping = true;
    }


    private void SetJumpFlamesSpawningOffset()
    {
        offsetFlames = transform.position;
        offsetFlames.y -= 0.2f;
    }
    private void RestartRespawnedBool()
    {
        if (respawned) respawned = false;
    }
    private void Respawn()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            GameObject.FindGameObjectWithTag("Checkpoint").GetComponent<CheckpointSystem>().RespawnPlayer();
            respawned = true;
        }
    }
    private void CheckIfJumping()
    {
        if (isGrounded) jumping = false;
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
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //If water, respawn player
        if (collision.gameObject.tag == "Water")
        {
            GameObject.FindGameObjectWithTag("Checkpoint").GetComponent<CheckpointSystem>().RespawnPlayer();

            dead = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Water")
        {
            dead = false;
        }
    }


}



