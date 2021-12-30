using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireManager : MonoBehaviour
{
    CheckpointSystem checkpointScript;
    public bool baseLight;

    void Start()
    {
        checkpointScript = GetComponent<CheckpointSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!checkpointScript.isBurning)
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
                baseLight = true;
            }
        }
        else
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
                baseLight = false;
            }
        }
    }
}
