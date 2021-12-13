using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDropController : MonoBehaviour
{


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);

        }
    }

}
