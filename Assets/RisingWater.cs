using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisingWater : MonoBehaviour
{
    public float yScaleStart, yScaleEnd;
    public bool isRising;
    PlayerMovement playerScript;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isRising)
        {
            gameObject.transform.localScale += (Vector3.up * Time.deltaTime);
            if (playerScript.dead)
            {
                isRising = false;
                gameObject.transform.localScale = (new Vector3(gameObject.transform.localScale.x, yScaleStart, gameObject.transform.localScale.z));
            }
        } 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isRising = true;
        }
    }


}
