using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icemeltscript : MonoBehaviour
{
    public float resetMeltingValue = 2f;

    public SpriteRenderer iceSprite;
    PlayerMovement playerScript;

    EdgeCollider2D iceBlockCollider;

    bool runTimer, restartIceBlocks;

    float currentMeltingValue, resetAlphaValue = 255f, currentAlpaValue;
   
    void Start()
    {
        currentMeltingValue = resetMeltingValue;
        iceBlockCollider = GetComponent<EdgeCollider2D>();

        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }
    
    void Update()
    {
        ResetIceBlocksComponentsAndValues();
        RunTimer();
        DefineSpriteOpacity();
        ChangeSpriteOpacity();
        CheckMeltingTimer();
    }

    private void ResetIceBlocksComponentsAndValues()
    {
        if (playerScript.dead || playerScript.respawned)
        {

            currentMeltingValue = resetMeltingValue;
            currentAlpaValue = resetAlphaValue;

            iceBlockCollider.enabled = true;

            restartIceBlocks = false;
        }
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
            iceBlockCollider.enabled = false;
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
