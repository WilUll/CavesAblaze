using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject jumpFlames;
    public Animator animator;

    public SpriteRenderer playerSpriteRenderer;
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

    public bool isGrounded, playerDead, respawned;

    bool isAttached;

    bool varSet = false;

    float waitingAnimationTime, resetWaitingAnimTime;
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        dash = GetComponent<DashController>();

        //jumpsCounter = GetComponent<JumpsCounter>();

        jumpsLeft = maxJumps;

        playerDead = false;

        resetWaitingAnimTime = 1.5f;
    }

    void Update()
    {
        xAxis = Input.GetAxisRaw("Horizontal");
        yAxis = Input.GetAxisRaw("Vertical");
        Jump();
        Timers();
        JumpsLeftLimiter();
        //JumpAnimation();

        if (respawned) respawned = false;
        Respawn();

        //jumpsCounter.CountingJumpsLeft();

        offsetFlames = transform.position;
        offsetFlames.y -= 0.2f;

        //animator.SetFloat("Speed", Mathf.Abs(xAxis));
        //if (xAxis < 0)
        //{
        //    playerSpriteRenderer.flipX = true;
        //    animator.SetBool("IsWaiting", false);
        //    waitingAnimationTime = resetWaitingAnimTime;
        //}
        //else if (xAxis > 0)
        //{
        //    playerSpriteRenderer.flipX = false;
        //    animator.SetBool("IsWaiting", false);
        //    waitingAnimationTime = resetWaitingAnimTime;
        //}
        //else if (xAxis == 0)
        //{
        //    waitingAnimationTime -= Time.deltaTime;
        //    if (waitingAnimationTime <= 0) WaitingAnimation();
        //}
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
                Instantiate(jumpFlames, transform.position, Quaternion.identity);
                Detach();

                //animator.SetBool("IsJumping", true);
            }
            if (Input.GetKey(KeyCode.Space) && jumpTimer > 0)
            {
                playerRB.velocity = Vector2.up * pressJumpPower;
               // animator.SetBool("IsJumping", true);
            }
        }
        else if (isAttached)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                playerRB.velocity = Vector2.up * minJumpPower;
                jumpTimer = jumpTimerValue;
                Detach();
            }
            if (Input.GetKey(KeyCode.Space) && jumpTimer > 0)
            {
                playerRB.velocity = Vector2.up * pressJumpPower;
                Detach();

            }
        }
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

    private void WaitingAnimation()
    {
        animator.SetBool("IsWaiting", true);
    }
    private void JumpAnimation()
    {
        if(isGrounded)
        {
            animator.SetBool("IsJumping", false);
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
            playerRB.velocity = Vector2.zero;
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



