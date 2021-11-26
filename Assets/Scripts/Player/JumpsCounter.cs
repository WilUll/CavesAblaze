using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JumpsCounter : MonoBehaviour
{
    public Text jumpsText;
    PlayerMovement2 jumps;

    private void Start()
    {
        jumps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement2>();
    }

    private void Update()
    {
        jumpsText.text = string.Format("JUMPS: {0:0}", jumps.jumpsLeft);
    }
}
