using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGeyser : MonoBehaviour
{
    GameObject waterSprite;
    Transform startPos, endPos;
    float startSpeed, backSpeed;
    bool isPlayerClose;
    bool isShooting = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerClose && !isShooting)
        {
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isPlayerClose = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isPlayerClose = false;
    }


}
