using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointSystem : MonoBehaviour
{
    bool isPlayerClose = false;
    public bool isBurning = false;
    GameObject[] checkpoints;
    GameObject[] jumpFlames;

    private void Start()
    {
        checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
    }

    private void Update()
    {
        if (isPlayerClose && !isBurning)
        {
            for (int i = 0; i < checkpoints.Length; i++)
            {
                CheckpointSystem checkpointScript = checkpoints[i].GetComponent<CheckpointSystem>();
                checkpointScript.isBurning = false;
                checkpointScript.isPlayerClose = false;
            }
            isBurning = true;
        }
        if (isPlayerClose)
        {
            RefillJump();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
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
        for (int i = 0; i < checkpoints.Length; i++)
        {
            CheckpointSystem checkpointScript = checkpoints[i].GetComponent<CheckpointSystem>();
            if (checkpointScript.isBurning)
            {
                GameObject.FindGameObjectWithTag("Player").transform.position = checkpoints[i].transform.position;
                RefillJump();
            }
        }
    }

    public void RefillJump()
    {
        var player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement2>();


        jumpFlames = GameObject.FindGameObjectsWithTag("jumpFlames");
        for (int i = 0; i < jumpFlames.Length; i++)
        {
            Destroy(jumpFlames[i]);
        }
        player.jumpsLeft = player.maxJumps;
    }


}
