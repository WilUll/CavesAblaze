using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallLevel4CloseManager : MonoBehaviour
{
    GameObject childGround;
    public PlayerMovement playerScript;

    private void Update()
    {
        if (playerScript.respawned)
        {
            childGround = gameObject.transform.GetChild(0).gameObject;

            childGround.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            childGround = gameObject.transform.GetChild(0).gameObject;

            childGround.SetActive(true);
        }
    }
}
