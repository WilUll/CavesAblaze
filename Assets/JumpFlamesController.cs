using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpFlamesController : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Water"))
        {
            Destroy(gameObject);
        }
    }
}
