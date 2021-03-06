using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchlightInteraction : MonoBehaviour
{
    bool isPlayerClose = false;
    public bool isBurning;
    
    PlayerMovement playerScript;

    public int torchlightJumps;

    private void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

        isBurning = true;
    }

    private void Update()
    {
        GivePlayerJumpsAndDeactivate();

        Reactivate();
    }


    private void GivePlayerJumpsAndDeactivate()
    {
        if (isPlayerClose && isBurning)
        {
            isBurning = false;
            playerScript.currentJumpsLeft += 2;
            playerScript.refilled = false;
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
        }
    }
    private void Reactivate()
    {
        if (isBurning == false)
        {
            if (playerScript.dead || playerScript.respawned || playerScript.refilled)
            {
                isBurning = true;
                foreach (Transform child in transform)
                {
                    child.gameObject.SetActive(true);
                }
            }
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


}
