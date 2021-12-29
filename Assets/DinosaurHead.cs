using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinosaurHead : MonoBehaviour
{
    public bool isPlayerOn;
    public float startRotation = -17;
    public float currentRotation;
    public float endRotation = -8;
    // Start is called before the first frame update
    void Start()
    {
        currentRotation = startRotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerOn && currentRotation < endRotation)
        {
            currentRotation += 10 * Time.deltaTime;
            transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y, currentRotation));
        }
        if (!isPlayerOn && currentRotation > startRotation)
        {
            currentRotation -= 20 * Time.deltaTime;
            transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y, currentRotation));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerOn = true;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerOn = false;
        }
    }


}
