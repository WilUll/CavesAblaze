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
    bool isplayeron;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        if (transform.position.y <= down.position.y)
        {
            balloondown = true;
        }
        else if (transform.position.y >= up.position.y)
        {
            balloondown = false;
        }
        if (isplayeron)
        {
            transform.position = Vector2.MoveTowards(transform.position, up.position, speed * Time.deltaTime);
        }
        else if (!isplayeron)
        {
            transform.position = Vector2.MoveTowards(transform.position, down.position, speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            isplayeron = true;

        }


    }

    private void OnTriggerExit2D(Collider2D other)
    {
        isplayeron = false;
    }





}
