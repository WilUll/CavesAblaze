using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flyScript : MonoBehaviour
{
    Rigidbody2D fly;
    // Start is called before the first frame update
    void Start()
    {
        fly = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        fly.velocity = (new Vector2(Random.Range(-4, 5), Random.Range(-4, 5)));
    }
}
