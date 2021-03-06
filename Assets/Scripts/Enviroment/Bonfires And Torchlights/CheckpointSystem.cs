using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointSystem : MonoBehaviour
{
    public bool isPlayerClose = false;
    public bool isBurning = false;
    GameObject[] checkpoints;
    GameObject[] jumpFlames;

    PlayerMovement playerScript;
    CameraMovement camMovementScript;

    private void Start()
    {
        checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");

        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

       // camMovementScript = GameObject.FindGameObjectWithTag("CameraParent").GetComponent<CameraMovement>();

    }

    private void Update()
    {
        if (isPlayerClose && !isBurning)
        {
            for (int i = 0; i < checkpoints.Length; i++)
            {
                CheckpointSystem checkpointScript = checkpoints[i].GetComponent<CheckpointSystem>();
                checkpointScript.isBurning = false;
            }
            isBurning = true;
        }
        if (isPlayerClose)
        {
            RefillJump();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerClose = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerClose = false;
        }
    }
    public void RespawnPlayer()
    {
        SceneScript.Instance.playerDeaths++;
        if (playerScript.isAttached)
        {
            playerScript.Detach();
        }
        for (int i = 0; i < checkpoints.Length; i++)
        {
            CheckpointSystem checkpointScript = checkpoints[i].GetComponent<CheckpointSystem>();
            if (checkpointScript.isBurning)
            {
                GameObject.FindGameObjectWithTag("Player").transform.position = checkpoints[i].transform.position + Vector3.up / 2;
                playerScript.respawned = true;
                playerScript.canDie = false;
                RefillJump();
            }
        }
        ResetRope();

        GameObject[] flames = GameObject.FindGameObjectsWithTag("Flame");
        foreach (GameObject flame in flames)
        {
            GameObject.Destroy(flame);
        }
        GameObject[] dashWalls = GameObject.FindGameObjectsWithTag("DashWall");
        foreach (GameObject dashWall in dashWalls)
        {
            dashWall.GetComponent<DashWallsDestroy>().ResetCrystals();
        }
    }

    public void ResetRope()
    {
        GameObject[] ropes = GameObject.FindGameObjectsWithTag("Rope");
        foreach (GameObject rope in ropes)
        {
            GameObject.Destroy(rope);
        }
        RopeScript[] script = FindObjectsOfType<RopeScript>();
        foreach (RopeScript scripts in script)
        {
            scripts.GenerateRope();
        }
    }

    public void RefillJump()
    {
        jumpFlames = GameObject.FindGameObjectsWithTag("jumpFlames");
        for (int i = 0; i < jumpFlames.Length; i++)
        {
            Destroy(jumpFlames[i]);
        }
        playerScript.currentJumpsLeft = playerScript.maxJumps;
        playerScript.refilled = true;
    }


}
