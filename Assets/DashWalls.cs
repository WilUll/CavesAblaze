using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashWalls : MonoBehaviour
{
    public int wallHP = 3;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            DashController dashScript = other.gameObject.GetComponent<DashController>();
            if (dashScript.dashOn)
            {
                wallHP--;
                if (wallHP <= 0)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
    // Start is called before the first frame update


}
