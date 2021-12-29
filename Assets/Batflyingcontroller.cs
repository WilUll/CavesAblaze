using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Batflyingcontroller : MonoBehaviour
{
    Animator bat;
    bool secondanimation;

    void Start()
    {
        bat = GetComponent<Animator>();

        if (gameObject.CompareTag("Bat"))
        {
            secondanimation = true;
            bat.SetBool("batters", secondanimation);
        }
    }


    void Update()
    {
        
    }
}
