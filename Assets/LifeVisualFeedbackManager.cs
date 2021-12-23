using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeVisualFeedbackManager : MonoBehaviour
{
    PlayerMovement playerScript;
    SpriteRenderer playerSpriterRenderer;

    float alphaValueWhenImmune = 0.5f, modulusAlpha = 1;

    void Start()
    {
        playerScript = GetComponent<PlayerMovement>();
        playerSpriterRenderer = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        if(playerScript.runImmunityTimer)
        {
            playerSpriterRenderer.color =new Color(playerSpriterRenderer.color.r, playerSpriterRenderer.color.g, playerSpriterRenderer.color.b, alphaValueWhenImmune % modulusAlpha);
            modulusAlpha -= 0.01f;
            if (modulusAlpha <= 0) modulusAlpha = 1;
        }
        else
        {
            playerSpriterRenderer.color = new Color(playerSpriterRenderer.color.r, playerSpriterRenderer.color.g, playerSpriterRenderer.color.b, 1);
        }
    }
}
