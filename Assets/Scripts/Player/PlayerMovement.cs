using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject jumpFlames;

    public Rigidbody2D playerRB;
    DashController dash;
    GameObject ropeObj;

    public bool isGrounded, dead, respawned, refilled,
                isAttached, jumping, oneDashOnAir, coroutineStart, canDie, runImmunityTimer;

    public float xAxis, yAxis, speed;
    public float holdJumpPower, minJumpPower, jumpTimerValue, jumpTimer, currentJumpsLeft, resetImmunityTimer;
    public int maxJumps, waitOneFrame = 2;

    public float immunityTimer, lastFrameAmountOfJumpsLeft;

    bool varSet = false;

    Vector3 offsetFlames;

    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        dash = GetComponent<DashController>();
        lastFrameAmountOfJumpsLeft = currentJumpsLeft;

        //jumpsLeft = maxJumps;

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

        GiveImmunityAfterLosingLastFlame();
        CheckIfRunImmunityTimer();
        SetCanDieFalseIfOneOrMoreJumpsLefts();

        CompareLastAmountOfJumpFlamesWithThisFrame();
    }

    private void SetCanDieFalseIfOneOrMoreJumpsLefts()
    {
        if (currentJumpsLeft > 0) canDie = false;
    }

    private void CheckIfRunImmunityTimer()
    {
        if(runImmunityTimer)
        {
            RunImmunityTimer();

            if (immunityTimer <= 0)
            {
                runImmunityTimer = false;
                immunityTimer = 0;

                if(currentJumpsLeft == 0) canDie = true;
            }
        }
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
            if (Input.GetButtonDown("Jump") && currentJumpsLeft > 0)
            {
                CalculateInitialJumpPower();
                ResetJumpTimer();
                SetTrueJumpConditions();
                currentJumpsLeft--;

                Instantiate(jumpFlames, offsetFlames, Quaternion.identity);
            }
            if (Input.GetButton("Jump") && jumpTimer > 0)
            {
                CalculateHoldJumpPower();
                SetTrueJumpConditions();
            }
        }
        else if (isAttached)
        {
            if (Input.GetButtonDown("Jump"))
            {
                CalculateInitialJumpPower();
                ResetJumpTimer();
                SetTrueJumpConditions();
            }
            if (Input.GetButton("Jump") && jumpTimer > 0)
            {
                CalculateHoldJumpPower();
                SetTrueJumpConditions();
                Detach();
            }
            if (yAxis > 0)
            {
                if (!coroutineStart)
                {
                    StartCoroutine(timer());

                    IEnumerator timer()
                    {
                        coroutineStart = true;
                        ClimbRope(1);
                        yield return new WaitForSeconds(0.2f);
                        coroutineStart = false;
                    }
                }
            }
            if (yAxis < 0)
            {
                if (!coroutineStart)
                {
                    StartCoroutine(timer());

                    IEnumerator timer()
                    {
                        coroutineStart = true;
                        ClimbRope(-1);
                        yield return new WaitForSeconds(0.2f);
                        coroutineStart = false;
                    }
                }
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

    private void ClimbRope(int dir)
    {
        RopeSegment ropeConnect = transform.parent.gameObject.GetComponent<RopeSegment>();
        GameObject newSeg = null;
        if (dir > 0)
        {
            if (ropeConnect.connectedAbove != null && ropeConnect.connectedAbove.gameObject.GetComponent<RopeSegment>() != null)
            {
                Debug.Log(ropeObj.GetComponent<RopeSegment>());
                newSeg = ropeConnect.connectedAbove;
            }
        }
        else
        {
            if (ropeConnect.connectedBelow != null)
            {
                newSeg = ropeConnect.connectedBelow;
            }
        }
        if (newSeg != null)
        {
            Detach();
            Attach(newSeg.GetComponent<Rigidbody2D>());
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
        if (currentJumpsLeft > maxJumps) currentJumpsLeft = maxJumps;
        if (currentJumpsLeft < 0) currentJumpsLeft = 0;
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
        if (respawned)
        {
            if(waitOneFrame == 0)
            {
                respawned = false;
                waitOneFrame = 2;
            }
            waitOneFrame--;
        }
    }
   
    private void Respawn()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            GameObject.FindGameObjectWithTag("Checkpoint").GetComponent<CheckpointSystem>().RespawnPlayer();
            respawned = true;
            waitOneFrame = 1;
        }
    }
    private void CheckIfJumping()
    {
        if (isGrounded) jumping = false;
    }

    private void CompareLastAmountOfJumpFlamesWithThisFrame()
    {
        if (lastFrameAmountOfJumpsLeft != currentJumpsLeft)
        {
            lastFrameAmountOfJumpsLeft = currentJumpsLeft;
        }
    }

    private void GiveImmunityAfterLosingLastFlame()
    {
        if (currentJumpsLeft == 0 && lastFrameAmountOfJumpsLeft == currentJumpsLeft + 1)
        {
            runImmunityTimer = true;
            ResetImmunityTimer();
        }
    }

    private void RunImmunityTimer()
    {
        if (currentJumpsLeft == 0) immunityTimer -= Time.deltaTime;
        else immunityTimer = 0;
    }

    public void ResetImmunityTimer()
    {
        immunityTimer = resetImmunityTimer;
        lastFrameAmountOfJumpsLeft = currentJumpsLeft; 
    }


    public void Attach(Rigidbody2D ropeBone)
    {
        ropeObj = ropeBone.gameObject;
        ropeObj.GetComponent<RopeSegment>().isPlayerConnected = true;
        isAttached = true;
        gameObject.transform.parent = ropeBone.transform;
    }

    //Detaches player from the rope and enables rb
    public void Detach()
    {
        ropeObj = transform.parent.gameObject;
        gameObject.transform.parent = null;
        StartCoroutine(varTimer());
        IEnumerator varTimer()
        {
            ropeObj.GetComponent<RopeSegment>().isPlayerConnected = false;
            varSet = false;
            isAttached = false;
            yield return new WaitForSeconds(0.2f);
            playerRB.gravityScale = 4;
            coroutineStart = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Rope attach
        if (!isAttached)
        {
            if (other.gameObject.tag == "Rope")
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
            dead = true;
        }
        if(collision.gameObject.tag == "WaterDrop" && canDie)
        {
            dead = true;
        }
    }
}