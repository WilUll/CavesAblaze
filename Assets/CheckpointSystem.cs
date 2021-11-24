using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointSystem : MonoBehaviour
{
    bool isPlayerClose = false;
    public bool isBurning = false;
    GameObject[] checkpoints;

    private void Start()
    {
        checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
    }

    private void Update()
    {
        if (isPlayerClose && !isBurning)
        {
            for (int i = 0; i < checkpoints.Length; i++)
            {
                CheckpointSystem checkpointScript = checkpoints[i].GetComponent<CheckpointSystem>();
                checkpointScript.isBurning = false;
            }
            isBurning = true;
        }
        if (isBurning)
        {
            transform.GetChild(0).gameObject.SetActive(!transform.GetChild(0).gameObject.active);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Fungerar");
            isPlayerClose = true;
        }
    }


}
