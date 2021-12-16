using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDropCreator : MonoBehaviour
{
    public float dropDelay, yOffset;
    public GameObject waterDrop;

    Vector2 positionPlusYOffset;

    void Start()
    {
        InvokeRepeating("SpawnWaterDrop", dropDelay, dropDelay);

        positionPlusYOffset.x = transform.position.x;
        positionPlusYOffset.y = transform.position.y + yOffset;
    }

    void SpawnWaterDrop()
    {   
       Instantiate(waterDrop, positionPlusYOffset, Quaternion.identity);
    }

}