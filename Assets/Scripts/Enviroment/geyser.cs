using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class geyser : MonoBehaviour
{
    public float boost = 25f;
    public GameObject gasLeak;
    public ParticleSystem explotion;
    public AudioClip[] clip;
    public AudioSource audioSource;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector3.up * boost, ForceMode2D.Impulse);

            gasLeak.SetActive(false);
            explotion.Play();
            audioSource.PlayOneShot(clip[0]);

        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        gasLeak.SetActive(true);
    }
}
