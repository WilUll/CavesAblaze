using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDropCreator : MonoBehaviour
{
    public float dropDelay;
    public GameObject waterDrop;

    void Start()
    {
        InvokeRepeating("SpawnWaterDrop", dropDelay, dropDelay);
    }

    void SpawnWaterDrop()
    {   
       Instantiate(waterDrop, transform.position, Quaternion.identity);
    }

}