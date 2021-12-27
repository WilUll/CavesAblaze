using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOnDeath : MonoBehaviour
{
    public Image fade;
    // Start is called before the first frame update
    void Awake()
    {
        fade.CrossFadeAlpha(0f, 0f, false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            fade.CrossFadeAlpha(1, 1, true);
        }
        
    }
}
