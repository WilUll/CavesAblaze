using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level5SecondToThirdManager : MonoBehaviour
{
    GameObject childGround;
    public GameObject secondPart, thirdPart;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        thirdPart.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        childGround = gameObject.transform.GetChild(0).gameObject;

        childGround.SetActive(true);
        secondPart.SetActive(false);
    }
}
