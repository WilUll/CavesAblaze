using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeFlames : MonoBehaviour
{
    GameObject player;
    PlayerMovement playerScript;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Destroys jumpflames and add jumps
        if (other.CompareTag("jumpFlames") && playerScript.jumpTimer <= 0)
        {
            Destroy(other.gameObject);
            playerScript.currentJumpsLeft++;
        }
    }
}

