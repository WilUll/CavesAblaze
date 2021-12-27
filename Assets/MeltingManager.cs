using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeltingManager : MonoBehaviour
{
    public float resetMeltingValue = 2f;

    public SpriteRenderer iceSprite;
    public Sprite[] spriteArray;
    PlayerMovement playerScript;

    EdgeCollider2D iceBlockCollider;
    BoxCollider2D iceBlockCollider2;

    bool runTimer, restartIceBlocks;

    float currentMeltingValue, resetAlphaValue = 255f, currentAlpaValue;

    void Start()
    {
        currentMeltingValue = resetMeltingValue;
        iceBlockCollider = GetComponent<EdgeCollider2D>();
        iceBlockCollider2 = GetComponent<BoxCollider2D>();

        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    void Update()
    {
        ResetIceBlocksComponentsAndValues();
        RunTimer();
        DefineSpriteOpacity();
        ChangeSpriteOpacity();
        ChooseSprite();
        CheckMeltingTimer();
    }

    private void ChooseSprite()
    {
        if (currentAlpaValue > 0.66f) iceSprite.sprite = spriteArray[0];
        else if (currentAlpaValue > 0.33f && currentAlpaValue < 0.66f) iceSprite.sprite = spriteArray[1];
        else iceSprite.sprite = spriteArray[2];
    }

    private void ResetIceBlocksComponentsAndValues()
    {
        if (playerScript.dead || playerScript.respawned)
        {

            currentMeltingValue = resetMeltingValue;
            currentAlpaValue = resetAlphaValue;

            iceBlockCollider.enabled = true;
            iceBlockCollider2.enabled = true;

            restartIceBlocks = false;
        }
    }

    private void ChangeSpriteOpacity()
    {
        iceSprite.color = new Color(1f, 1f, 1f, currentAlpaValue);
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
            iceBlockCollider2.enabled = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("jumpFlames"))
        {
            runTimer = true;
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("jumpFlames"))
        {
            runTimer = false;
        }
    }
}
