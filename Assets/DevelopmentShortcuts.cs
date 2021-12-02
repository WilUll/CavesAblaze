using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevelopmentShortcuts : MonoBehaviour
{
    GameObject player;
    PlayerMovement2 playerScript;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<PlayerMovement2>();
    }

    void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                playerScript.enabled = false;
            }
        }
}
