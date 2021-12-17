using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearController : MonoBehaviour
{
    GameObject childGround;
    private void OnTriggerExit2D(Collider2D collision)
    {
        childGround = gameObject.transform.GetChild(0).gameObject;

        childGround.SetActive(true);
    }
}
