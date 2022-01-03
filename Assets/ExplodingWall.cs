using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingWall : MonoBehaviour
{
    public int health;
    public GameObject shatter;
    ParticleSystem shatterSystem;
    public SpriteRenderer wallSprite;
    public Sprite[] spriteArray;
    public AudioSource audioSource;

    BoxCollider2D boxCollider;
    PolygonCollider2D polygonCollider;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        polygonCollider = GetComponent<PolygonCollider2D>();
        shatterSystem = shatter.GetComponent<ParticleSystem>();
        audioSource = GetComponent<AudioSource>();
    }

    public void DamageWall(Transform positionSystem)
    {
        health--;
        shatter.transform.position = new Vector3(positionSystem.position.x - 0.5f, positionSystem.position.y, positionSystem.position.z);
        shatterSystem.Play();

        switch (health)
        {
            case 0:
                wallSprite.sprite = spriteArray[2];
                boxCollider.enabled = false;
                polygonCollider.enabled = false;
                break;
            case 1:
                wallSprite.sprite = spriteArray[1];
                boxCollider.enabled = false;
                polygonCollider.enabled = true;
                Debug.Log("1");
                break;
            case 2:
                wallSprite.sprite = spriteArray[0];
                boxCollider.enabled = true;
                polygonCollider.enabled = false;

                break;

        }
    }
}
