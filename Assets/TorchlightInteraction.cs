using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchlightInteraction : MonoBehaviour
{
    bool isPlayerClose = false;
    public bool isBurning;
    
    PlayerMovement2 playerScript;

    public int torchlightJumps;

    private void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement2>();

        isBurning = true;
    }

    private void Update()
    {
        if (isPlayerClose && isBurning)
        {
            RefillJump();
            isBurning = false;
        }

        if (playerScript.playerDead)
        {
            isBurning = true;
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

    public void RefillJump()
    {
        playerScript.jumpsLeft += torchlightJumps;
    }


}
