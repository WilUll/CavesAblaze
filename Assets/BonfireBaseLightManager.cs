using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonfireBaseLightManager : MonoBehaviour
{
    public FireManager fireManagerScript;
    void Start()
    {
        
    }
    void Update()
    {
        if(fireManagerScript.baseLight)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
