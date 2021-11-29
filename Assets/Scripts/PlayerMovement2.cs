using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2 : MonoBehaviour
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

    bool isAttached = false;
    GameObject ropeObj;
    bool varSet = false;
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
        if (!isAttached)
        {
            if (Input.GetKeyDown(KeyCode.Space) && jumpsLeft > 0)
            {
                player.velocity = Vector2.up * minJumpPower;
                jumpTimer = jumpTimerValue;
                jumpsLeft--;
                Instantiate(jumpFlames, offsetFlames, Quaternion.identity);
                Detach();
            }
            if (Input.GetKey(KeyCode.Space) && jumpTimer > 0)
            {
                player.velocity = Vector2.up * minJumpPower;
            }
        }
        else if (isAttached)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                player.velocity = Vector2.up * minJumpPower;
                jumpTimer = jumpTimerValue;
                Detach();
            }
            if (Input.GetKey(KeyCode.Space) && jumpTimer > 0)
            {
                player.velocity = Vector2.up * minJumpPower;
                Detach();

            }
        }

    }

    private void FixedUpdate()
    {
        xAxis = Input.GetAxisRaw("Horizontal");
        float yAxis = Input.GetAxisRaw("Vertical");
        if (!dash.dashOn && !isAttached)
        {
            player.velocity = new Vector2(xAxis * speed, player.velocity.y);
        }

        if (isAttached)
        {
            player.transform.position = ropeObj.transform.position;
            player.GetComponent<BoxCollider2D>().enabled = false;
            player.gravityScale = 0;
            varSet = true;
        }
        if (isAttached && varSet)
        {
            ropeObj.GetComponent<Rigidbody2D>().AddForce(new Vector2(xAxis * speed, 0));

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
            player.GetComponent<BoxCollider2D>().enabled = true;
            player.gravityScale = 4;
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

        //If water, respawn player
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



