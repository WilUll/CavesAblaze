using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class icemeltscript : MonoBehaviour
{
    public float Melting = 50f;

    public GameObject sprite; 

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Melting--;
            Debug.Log(Melting);

            if (Melting <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
