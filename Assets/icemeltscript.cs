using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class icemeltscript : MonoBehaviour
{
    public float melting = 5f;

    public GameObject sprite;
   
    void RunTimer()
    {
        melting -= Time.deltaTime;
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            RunTimer();

            if (melting <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
