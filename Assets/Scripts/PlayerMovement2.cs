using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2 : MonoBehaviour
{
    private Rigidbody2D player;
    public float speed;
    public float jumpForce;
    float xAxis;

    private void Start()
    {
        player = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        xAxis = Input.GetAxisRaw("Horizontal");
        player.velocity = new Vector2(xAxis * speed, player.velocity.y);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            player.velocity = Vector2.up * jumpForce;
        }
    }

}
