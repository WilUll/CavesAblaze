using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOnDeath : MonoBehaviour
{
    public GameObject player;
    PlayerMovement playerScript;
    public Image fade;
    // Start is called before the first frame update
    void Start()
    {
        playerScript = player.GetComponent<PlayerMovement>();
        fade.CrossFadeAlpha(0f, 0f, false);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerScript.dead)
        {
            StartCoroutine(fadeOut());
        }
    }

    IEnumerator fadeOut()
    {
        playerScript.dead = false;
        playerScript.playerRB.velocity = Vector2.zero;
        playerScript.enabled = false;
        fade.CrossFadeAlpha(1, 0.25f, true);
        yield return new WaitForSeconds(0.5f);
        GameObject.FindGameObjectWithTag("Checkpoint").GetComponent<CheckpointSystem>().RespawnPlayer();
        playerScript.enabled = true;
        fade.CrossFadeAlpha(0, 0.5f, true);
    }
}
