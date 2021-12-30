using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingWall : MonoBehaviour
{
    public int health;

    public void DamageWall()
    {
        health--;
        if (health == 0)
        {
            gameObject.SetActive(false);
        }
    }
}
