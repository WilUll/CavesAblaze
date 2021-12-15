using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashWallsDestroy : MonoBehaviour
{
    public int wallHP = 3;
    public Sprite[] spriteArray;
    public float timerReset, xPositionSprites012, xPositionSprite3;

    float timer;

    int indexNumber = 0;

    BoxCollider2D boxColliderSprite0And1;
    PolygonCollider2D polygonColliderSprite2;
    CircleCollider2D circleColliderSprite3;
    CapsuleCollider2D capsuleCollider2DSprite3;
    SpriteRenderer spriteRenderer;
    GameObject particleShatter;
    ParticleSystem shatter;

    private void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        boxColliderSprite0And1 = gameObject.GetComponent<BoxCollider2D>();
        polygonColliderSprite2 = gameObject.GetComponent<PolygonCollider2D>();
        circleColliderSprite3 = gameObject.GetComponent<CircleCollider2D>();
        capsuleCollider2DSprite3 = gameObject.GetComponent<CapsuleCollider2D>();
        particleShatter = GameObject.FindGameObjectWithTag("ShatterParticles");
        shatter = particleShatter.GetComponent<ParticleSystem>();

        spriteRenderer.sprite = spriteArray[indexNumber];
    }
    private void Update()
    {
        RunTimer();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        CheckCollisionWithPlayer(other);
    }

    private void CheckCollisionWithPlayer(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            DashController dashScript = other.gameObject.GetComponent<DashController>();
            ContinueIfPlayerIsDashing(dashScript);
        }
    }

    private void ContinueIfPlayerIsDashing(DashController dashScript)
    {
        if (dashScript.dashOn && timer <= 0)
        {
            ResetTimer();
            ReduceHP();
            NextSprite();
            PlayParticleAnimation();
            HandleColliders(indexNumber);
        }
    }


    private void RunTimer()
    {
        if (timer > 0) timer -= Time.deltaTime;
    }
    private void ResetTimer()
    {
        timer = timerReset;
    }
    private void ReduceHP()
    {
        wallHP--;
    }
    private void NextSprite()
    {
        indexNumber++;
        spriteRenderer.sprite = spriteArray[indexNumber];
    }
    private void PlayParticleAnimation()
    {
        //SelectParticlePosition(indexNumber);

        shatter.Play();
    }
    private void HandleColliders(int indexNumber)
    {
        switch (indexNumber)
        {
            case 0:
                boxColliderSprite0And1.enabled = true;
                polygonColliderSprite2.enabled = false;
                circleColliderSprite3.enabled = false;
                capsuleCollider2DSprite3.enabled = false;
                break;
            case 1:
                boxColliderSprite0And1.enabled = true;
                polygonColliderSprite2.enabled = false;
                circleColliderSprite3.enabled = false;
                capsuleCollider2DSprite3.enabled = false;
                break;
            case 2:
                boxColliderSprite0And1.enabled = false;
                polygonColliderSprite2.enabled = true;
                circleColliderSprite3.enabled = false;
                capsuleCollider2DSprite3.enabled = false;
                break;
            case 3:
                boxColliderSprite0And1.enabled = false;
                polygonColliderSprite2.enabled = false;
                circleColliderSprite3.enabled = true;
                capsuleCollider2DSprite3.enabled = true;
                break;
        }

    }

    private void SelectParticlePosition(int indexNumber)
    {
        if (indexNumber == 2) particleShatter.transform.position *= -1;
    }

    
}
