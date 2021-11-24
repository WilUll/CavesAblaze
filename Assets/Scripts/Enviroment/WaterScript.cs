using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterScript : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject bonfirePrefab;
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Flammable"))
        {   
            other.GetComponent<fireSpreadScript>().fuel=0;
        }
    }

}
