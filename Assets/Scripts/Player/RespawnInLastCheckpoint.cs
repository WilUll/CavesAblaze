using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnInLastCheckpoint : MonoBehaviour
{
    CheckpointSystem checkpoint;
    void Start()
    {
        checkpoint = GetComponent<CheckpointSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        { 
            checkpoint.RespawnPlayer();
        }
    }
}
