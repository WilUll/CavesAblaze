using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class JumpsCounter : MonoBehaviour
{
    public TextMeshProUGUI jumpsText;
    PlayerMovement jumps;
    int lastJumps;
    Animator animatorImage;

    private void Start()
    {
        jumps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        animatorImage = jumpsText.gameObject.transform.parent.GetComponent<Animator>();
    }

    private void Update()
    {
        jumpsText.text = jumps.currentJumpsLeft.ToString();
        if (lastJumps > (int)jumps.currentJumpsLeft)
        {
            animatorImage.Play("Base Layer.UiAnimation", 0, 0);
        }
        lastJumps = (int)jumps.currentJumpsLeft;
    }
}
