using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevScript : MonoBehaviour
{
    public GameObject player;
    bool isGravity = false;
    int oldJumps;
    PlayerMovement playerScript;
    // Start is called before the first frame update
    void Start()
    {
        playerScript = player.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (isGravity)
            {
                player.GetComponent<Rigidbody2D>().gravityScale = 0;
                oldJumps = playerScript.jumpsLeft;
                playerScript.jumpsLeft = 0;
                isGravity = false;
            }
            else
            {
                player.GetComponent<Rigidbody2D>().gravityScale = 4;
                playerScript.jumpsLeft = oldJumps;
                isGravity = true;
            }
        }
    }
}
