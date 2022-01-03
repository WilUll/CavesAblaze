using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDropController : MonoBehaviour
{
    GameObject player;
    PlayerMovement playerScript;
    AudioSource audioSource;
    
    public SpriteRenderer spriteDrop;
    public BoxCollider2D colliderDrop;
    public GameObject spriteLight;

    public AudioClip[] clip;

    public GameObject jumpFlames;
    Vector3 offsetFlames;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<PlayerMovement>();
        audioSource = GetComponent<AudioSource>();

    }

    private void Update()
    {
        Setflamesoffset();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.CompareTag("Player"))
        {

            if (playerScript.currentJumpsLeft > 0)
            {
                GameObject currentFlame = Instantiate(jumpFlames, offsetFlames, Quaternion.identity);
                currentFlame.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 4, ForceMode2D.Impulse);
            }
            Destroy(gameObject);
            playerScript.currentJumpsLeft--;
        }
        else 
        {
            colliderDrop.enabled = false;
            spriteDrop.enabled = false;
            spriteLight.SetActive(false);

            float randomPitch = Random.Range(0.8f, 1.2f);

            int index = Random.Range(0, 4);

            StartCoroutine(playWaterDrop());
            IEnumerator playWaterDrop()
            {
                audioSource.pitch = randomPitch;
                audioSource.PlayOneShot(clip[index]);
                yield return new WaitForSeconds(clip[index].length);
                Destroy(gameObject);
            }
        }
    }


    private void Setflamesoffset()
    {
        offsetFlames = player.transform.position;
        offsetFlames.y += 3f;
        //offsetFlames.x -= 2f;
    }

}
