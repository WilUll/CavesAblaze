using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisingWater : MonoBehaviour
{
    public float yScaleStart, yScaleEnd, yPositionStart, yPositionEnd;
    public bool isRising, startTheRise;

    public float timer, resetTimer;

    PlayerMovement playerScript;
    AudioSource audioSource;

    Vector3 originalPosition;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        audioSource = GetComponent<AudioSource>();

        originalPosition = gameObject.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isRising) audioSource.Stop();

        if (isRising && !startTheRise)
        {
            timer -= Time.deltaTime;
        }

        if (timer <= 0)
        {
            startTheRise = true;
            timer = resetTimer;
        }

        if (isRising && startTheRise)
        {
            if (gameObject.transform.localPosition.y >= yPositionEnd)
            {
            }
            else
            {
                gameObject.transform.localPosition += (Vector3.up * Time.deltaTime);
            }
            if (gameObject.transform.localScale.y >= yScaleEnd)
            {
                isRising = false;
            }
            else
            {
                gameObject.transform.localScale += (Vector3.up * Time.deltaTime);
            }

        }
        if (playerScript.respawned)
        {
            isRising = false;
            startTheRise = false;
            gameObject.transform.localScale = (new Vector3(gameObject.transform.localScale.x, yScaleStart, gameObject.transform.localScale.z));
            gameObject.transform.localPosition = originalPosition;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isRising = true;

            audioSource.Play();
        }
    }


}
