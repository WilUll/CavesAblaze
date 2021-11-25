using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalBehaviour : MonoBehaviour
{
    Transform player;
    Transform portal2;
    bool teleport;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        portal2 = GameObject.FindGameObjectWithTag("Portal 2").GetComponent<Transform>();

    }
    void Update()
    {
        if(teleport && Input.GetKeyDown(KeyCode.E))
        {
            player.transform.position = portal2.transform.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            teleport = true;
        }
    }
}
