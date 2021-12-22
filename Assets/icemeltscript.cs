using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class icemeltscript : MonoBehaviour
{
    public float resetMeltingValue = 2f;

    public SpriteRenderer iceSprite;

    bool runTimer;

    float currentMeltingValue, resetAlphaValue = 255f, currentAlpaValue;
   
    void Start()
    {
        currentMeltingValue = resetMeltingValue;
        //iceSprite.color = new Color(42f, 71f, 229f, resetAlphaValue);
    }
    
    void Update()
    {
        RunTimer();
        DefineSpriteOpacity();
        ChangeSpriteOpacity();
        CheckMeltingTimer();
    }

    private void ChangeSpriteOpacity()
    {
        iceSprite.color = new Color(0f, 0f, 1f, currentAlpaValue);
    }

    private void DefineSpriteOpacity()
    {
        var percentageChange = currentMeltingValue / resetMeltingValue;

        currentAlpaValue = percentageChange;
    }

    void RunTimer()
    {
        if (runTimer)
        {
            currentMeltingValue -= Time.deltaTime;
        }
    }

    void CheckMeltingTimer()
    {
        if (currentMeltingValue <= 0)
        {
            gameObject.SetActive(false);
        }
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            runTimer = true;
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            runTimer = false;
        }
    }
}
