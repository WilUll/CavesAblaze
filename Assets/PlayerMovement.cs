using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5;
    public float jumpPower = 5;

    Rigidbody2D player;
    Vector2 movement = new Vector2();
    bool grounded;

    void Start()
    {
        player = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && grounded)
        {
            player.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }

        if (Input.GetButtonDown("Fire3"))
        {
            if (x < 0) player.AddForce(Vector2.left * jumpPower, ForceMode2D.Impulse);
            if (x > 0) player.AddForce(Vector2.right * jumpPower, ForceMode2D.Impulse);
            Debug.Log ("Dashing baby");
        }

        movement.x = x * speed;



    }
    private void FixedUpdate()
    {
        movement.y = player.velocity.y;
        player.velocity = movement;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        grounded = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        grounded = false;
    }
}
