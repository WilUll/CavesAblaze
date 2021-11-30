using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDrop : MonoBehaviour
{
    public float dropDelay;
    bool isDripping = false;
    GameObject waterDrop;

    // Start is called before the first frame update
    void Start()
    {
        waterDrop = GameObject.FindGameObjectWithTag("WaterDrop");
    }

    // Update is called once per frame
    void Update()
    {
        if (isDripping)
        {

        }
    }

    void SpawnWaterDrop()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isDripping = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isDripping = false;
    }


}