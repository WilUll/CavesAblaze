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
                if (Vector2.Distance(transform.position, flammableObjects[i].transform.position) <= 3)
                {
                    fireSpreadScript flameScript = flammableObjects[i].GetComponent<fireSpreadScript>();
                    if (!flameScript.objectBurn)
                    {
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
        else if (objectBurn && isBurning)
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
                //Destroy();
                objectBurn = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        objectBurn = true;
    }
}

