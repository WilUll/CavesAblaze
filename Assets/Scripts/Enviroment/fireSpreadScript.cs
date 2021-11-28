using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireSpreadScript : MonoBehaviour
{
    //Fire variables
    public float fuel;
    public GameObject fire;
    bool isBurning;
    public float burnTimer;
    public bool objectBurn = false;

    //Flammable check variables
    GameObject[] flammableObjects;

    private void Start()
    {
        flammableObjects = GameObject.FindGameObjectsWithTag("Flammable");
    }



    private void Update()
    {
        if (objectBurn)
        {
            IgniteObject();

            for (int i = 0; i < flammableObjects.Length; i++)
            {
                if (Vector2.Distance(transform.position, flammableObjects[i].transform.position) <= 2)
                {
                    fireSpreadScript flameScript = flammableObjects[i].GetComponent<fireSpreadScript>();
                    if (!flameScript.objectBurn)
                    {
                        StartCoroutine(StartTimer());
                        
                    }
                    IEnumerator StartTimer()
                    {
                        yield return new WaitForSeconds(1);
                        flameScript.objectBurn = true;
                    }
                }
            }
        }
    }

    private void IgniteObject()
    {
        if (!isBurning && fuel > 0)
        {
            var fireObject = Instantiate(fire, gameObject.transform.position, Quaternion.identity);
            fireObject.transform.parent = gameObject.transform;
            isBurning = true;
        }
        else if (isBurning)
        {

            if (isBurning && fuel > 0)
            {
                burnTimer -= Time.deltaTime;

                if (burnTimer <= 0)
                {
                    Destroy(gameObject);
                    objectBurn = false;

                }
            }
            else if (fuel <= 0 && isBurning)
            {
                Destroy(gameObject.transform.GetChild(0).gameObject);
                objectBurn = false;
                isBurning = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            objectBurn = true;
        }
        if (collision.CompareTag("Water"))
        {
            fuel = 0;
        }
    }

}