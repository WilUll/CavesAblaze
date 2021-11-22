using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb2d;
    float speed = 5;

    Vector2 movement;

    float x, y;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        movement = new Vector3(0, 0);
    }

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        rb2d.velocity = movement * speed;
    }
}
