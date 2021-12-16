using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDropController : MonoBehaviour
{
    GameObject player;
    PlayerMovement playerScript;
    public GameObject jumpFlames;
    Vector3 offsetFlames;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<PlayerMovement>();

    }

    private void Update()
    {
        Setflamesoffset();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Player"))
        {

            Destroy(gameObject);
            playerScript.jumpsLeft--;
            Instantiate(jumpFlames, offsetFlames, Quaternion.identity);

        }
    }


    private void Setflamesoffset()
    {
        offsetFlames = player.transform.position;
        offsetFlames.y -= 0.2f;
        offsetFlames.x -= 2f;
    }

}
