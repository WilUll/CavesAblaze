using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDrop : MonoBehaviour
{
    public float dropDelay;
    bool isDripping = false;
    public GameObject waterDrop;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnWaterDrop", dropDelay, dropDelay);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void SpawnWaterDrop()
    {   
        var instance = Instantiate(waterDrop, transform.position, Quaternion.identity);
        Destroy(instance, dropDelay);
    }

}