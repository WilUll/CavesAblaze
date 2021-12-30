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
    int resetWallHP;

    public int indexNumber = 0;

    public AudioSource audioSource;
    public AudioClip[] clip;

    BoxCollider2D boxColliderSprite0And1;
    PolygonCollider2D polygonColliderSprite2;

    SpriteRenderer spriteRenderer;
    GameObject particleShatter;
    ParticleSystem shatter;

    DashCrystalLightController lightPickSprite;



    private void Start()
    {
        boxColliderSprite0And1 = gameObject.GetComponent<BoxCollider2D>();
        polygonColliderSprite2 = gameObject.GetComponent<PolygonCollider2D>();

        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        particleShatter = gameObject.transform.Find("ParticlesShatter").gameObject;
        shatter = particleShatter.GetComponent<ParticleSystem>();

        lightPickSprite = gameObject.GetComponent<DashCrystalLightController>();

        spriteRenderer.sprite = spriteArray[indexNumber];

        resetWallHP = wallHP;
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
            PlayShatterSound();
            HandleColliders(indexNumber);
            lightPickSprite.HandleLights(indexNumber);
        }
    }

    private void PlayShatterSound()
    {
        audioSource.PlayOneShot(clip[0]);
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
        shatter.Play();
    }
    private void HandleColliders(int indexNumber)
    {
        switch (indexNumber)
        {
            case 0:
                boxColliderSprite0And1.enabled = true;
                polygonColliderSprite2.enabled = false;
                break;
            case 1:
                boxColliderSprite0And1.enabled = true;
                polygonColliderSprite2.enabled = false;
                break;
            case 2:
                boxColliderSprite0And1.enabled = false;
                polygonColliderSprite2.enabled = true;
                break;
            case 3:
                boxColliderSprite0And1.enabled = false;
                polygonColliderSprite2.enabled = false;
                break;
        }

    }
    public void ResetCrystals()
    {
        indexNumber = 0;
        wallHP = resetWallHP;

        boxColliderSprite0And1.enabled = true;
        polygonColliderSprite2.enabled = false;

        spriteRenderer.sprite = spriteArray[indexNumber];

        lightPickSprite.HandleLights(indexNumber);

    }
}
