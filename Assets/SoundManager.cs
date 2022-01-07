using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    Rigidbody2D rb2D;
    AudioSource audioSource;
    PlayerMovement playerScript;
    DashController dashScript;
    public AudioClip[] clips;

    private bool isMoving;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb2D = GetComponent<Rigidbody2D>();
        playerScript = GetComponent<PlayerMovement>();
        dashScript = GetComponent<DashController>();
    }

    void Update()
    {
        if (rb2D.velocity.x != 0) isMoving = true;

        else isMoving = false;

        if (Input.GetButtonDown("Fire3") && dashScript.dashCooldown <= 0 && dashScript.wallDashCooldown <= 0) audioSource.PlayOneShot(clips[0]);
        //else if (dashScript.dashOn == false) audioSource.Stop(clips[0]);

        if (Input.GetButtonDown("Jump") && playerScript.currentJumpsLeft > 0 && !playerScript.isAttached)
        {
            switch (playerScript.currentJumpsLeft)
            {
                case 4:
                    audioSource.PlayOneShot(clips[1]);
                    break;
                case 3:
                    audioSource.PlayOneShot(clips[2]);
                    break;
                case 2:
                    audioSource.PlayOneShot(clips[3]);
                    break;
                case 1:
                    audioSource.PlayOneShot(clips[4]);
                    break;
                default:
                    int index = Random.Range(1, 4);
                    audioSource.PlayOneShot(clips[index]);
                    break;
            }
        }
    }
}
