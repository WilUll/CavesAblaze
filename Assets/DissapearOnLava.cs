using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissapearOnLava : MonoBehaviour
{
    public SpriteRenderer feetRender;
    public bool onLava;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Lava"))
        {
            feetRender.enabled = false;
            onLava = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Lava"))
        {
            feetRender.enabled = true;
            onLava = false;
        }
    }

}
