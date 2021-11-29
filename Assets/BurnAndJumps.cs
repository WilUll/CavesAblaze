using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnAndJumps : MonoBehaviour
{
    public GameObject jumpFlames;

    bool isPlayerClose, isBurning, burnAlreadyStarted, burnPlant;

    public float burnPlantTime, burnPlantOffset;

    float burnTimer;

    Vector2 jumpFlamesInstantiatePosition;

    public Vector2 offsetFlame1;
    public Vector2 offsetFlame2;

    BoxCollider2D boxCollider;


    void Start()
    {
        burnTimer = burnPlantTime + burnPlantOffset;
        jumpFlamesInstantiatePosition = transform.position;

        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (burnAlreadyStarted)
        {
            burnPlantTime -= Time.deltaTime;
        }

        if(isPlayerClose && !isBurning && burnTimer > 0)
        {
            isBurning = true;
            burnAlreadyStarted = true;
            burnPlant = true;
        }

        if(isBurning && burnPlantTime <= 0)
        {
            isBurning = false;
        }

        Burn();
        CreateJumpFlames();
    }

    void Burn()
    {
        if (isBurning && burnPlant)
        {
            Destroy(gameObject, burnTimer);
            burnPlant = false;
        }
    }

    void CreateJumpFlames()
    {
        if (burnAlreadyStarted && !isBurning)
        {
            Instantiate (jumpFlames, jumpFlamesInstantiatePosition + offsetFlame1, Quaternion.identity);
            Instantiate(jumpFlames, jumpFlamesInstantiatePosition + offsetFlame2, Quaternion.identity);
            //It is broken, ask Luis why. (Hint: he doesn´t know.)
           
            burnAlreadyStarted = false;
            boxCollider.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerClose = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerClose = false;
        }
    }
}
