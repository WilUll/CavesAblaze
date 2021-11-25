using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalBehaviour : MonoBehaviour
{
    public Transform player;
    public Transform portal2;
    bool teleport;

    void Start()
    {
        player = GetComponent<Transform>();
        portal2 = GetComponent<Transform>();

    }
    void Update()
    {
        if(teleport && Input.GetKeyDown(KeyCode.E))
        {
            player.transform.position = portal2.transform.position;
            teleport = false;
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
