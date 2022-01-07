using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOnDeath : MonoBehaviour
{
    public GameObject playerBody;
    PlayerMovement playerScript;
    public Image fade;
    public bool playerSpawned = false;
    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        fade.CrossFadeAlpha(1f, 0f, false);
        playerSpawned = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerScript.dead)
        {
            StartCoroutine(fadeOut());
        }
        if (playerSpawned)
        {
            fade.CrossFadeAlpha(0, 2f, true);
            playerSpawned = false;
        }
    }

    IEnumerator fadeOut()
    {
        playerScript.dead = false;
        playerBody.SetActive(false);
        playerScript.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        playerScript.playerRB.velocity = Vector2.zero;
        playerScript.enabled = false;
        fade.CrossFadeAlpha(1, 1f, true);
        yield return new WaitForSeconds(1f);
        playerBody.SetActive(true);
        GameObject.FindGameObjectWithTag("Checkpoint").GetComponent<CheckpointSystem>().RespawnPlayer();
        playerScript.gameObject.GetComponent<BoxCollider2D>().enabled = true;
        playerScript.enabled = true;
        fade.CrossFadeAlpha(0, 0.5f, true);
    }
}
