using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterScript : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject bonfirePrefab;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(other.gameObject);
            Instantiate(playerPrefab, bonfirePrefab.transform.position, Quaternion.identity);
        }
    }

}
