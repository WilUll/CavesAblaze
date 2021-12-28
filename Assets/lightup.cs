using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightup : MonoBehaviour
{

    SpriteRenderer light;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        light = GetComponent<SpriteRenderer>();
    }
}
