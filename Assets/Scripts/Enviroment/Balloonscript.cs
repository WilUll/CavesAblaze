using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloonscript : MonoBehaviour
{

    public Transform player;

    public Transform balloon;

    public Transform up;

    public Transform down;


    public float speed;
    bool balloondown;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        Startballoon();
    }
    void Startballoon()
    {
        if(Vector2.Distance(player.position, balloon.position) < 0.5f && Input.GetKeyDown("e"))
        {
            if(transform.position.y <= down.position.y)
            {
                balloondown = true;
            }
            else if(transform.position.y >= up.position.y)
            {
                balloondown = false;
            }
        }

        if (balloondown)
        {
            transform.position = Vector2.MoveTowards(transform.position, up.position, speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, down.position, speed * Time.deltaTime);
        }
    }

}
