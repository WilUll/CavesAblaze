using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDropDestroyer : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }

}
