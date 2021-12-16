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

        if (other.gameObject.CompareTag("Player"))
        {

            Destroy(gameObject);
            playerScript.jumpsLeft--;
            GameObject currentFlame = Instantiate(jumpFlames, offsetFlames, Quaternion.identity);
            currentFlame.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 4, ForceMode2D.Impulse);
        }
        else 
        {
            Destroy(gameObject);
        }
    }


    private void Setflamesoffset()
    {
        offsetFlames = player.transform.position;
        offsetFlames.y += 3f;
        //offsetFlames.x -= 2f;
    }

}
