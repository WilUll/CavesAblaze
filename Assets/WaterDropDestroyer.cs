using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDropDestroyer : MonoBehaviour
{
    float destroy = 2;

    WaterDropCreator waterDrop;
    // Start is called before the first frame update
    void Start()
    {
        waterDrop = GetComponent<WaterDropCreator>();

        Destroy(gameObject, destroy);
    }
        
}
