using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class geyser : MonoBehaviour
{
    private float boost = 25f;
    public GameObject target;

    float angle;

    Vector2 dir;

    void Update()
    {
        dir = target.transform.position - transform.position;
        dir.Normalize();
        //angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

    }
   

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Rigidbody2D>().transform.Translate(dir * boost);

            Debug.Log(transform.right);
            Debug.Log(dir);

        }
    }
}
