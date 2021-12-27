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
        flammableObjects = GameObject.FindGameObjectsWithTag("Rope");
    }



    private void Update()
    {
        if (objectBurn)
        {
            IgniteObject();

            for (int i = 0; i < flammableObjects.Length; i++)
            {

                if (flammableObjects[i] != null && Vector2.Distance(transform.position, flammableObjects[i].transform.position) <= 2)
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
                    if (gameObject.transform.childCount > 1 )
                    {
                        for (int i = 0; i < gameObject.transform.childCount; i++)
                        {
                            if (gameObject.transform.GetChild(i).CompareTag("Player"))
                            {
                                gameObject.transform.GetChild(i).GetComponent<PlayerMovement>().Detach();
                            }
                        }
                    }

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
        if (collision.CompareTag("PlayerFeet"))
        {
            objectBurn = true;
        }
        if (collision.CompareTag("Water"))
        {
            fuel = 0;
        }
    }

}