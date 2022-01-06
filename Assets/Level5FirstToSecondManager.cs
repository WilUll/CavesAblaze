using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level5FirstToSecondManager : MonoBehaviour
{
    GameObject childGround;
    public GameObject firstPart, secondPart;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        secondPart.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        childGround = gameObject.transform.GetChild(0).gameObject;

        childGround.SetActive(true);
        firstPart.SetActive(false);
    }
}

