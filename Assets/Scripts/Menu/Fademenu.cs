using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fademenu : MonoBehaviour
{
    [SerializeField] private CanvasGroup mycanvas;

    [SerializeField] private bool fadein = false;
    [SerializeField] private bool fadeout = false;


    public void ShowUI()
    {
        fadein = true;
    }
    
    public void HideUI()
    {
        fadeout = true;
    }


    private void Update()
    {
        if (fadein)
        {
            if (mycanvas.alpha < 1)
            {
                mycanvas.alpha += Time.deltaTime;
                if(mycanvas.alpha >= 1)
                {
                    fadein = false;
                }
            }
        }
        if (fadeout)
        {
            if (mycanvas.alpha >= 0)
            {
                mycanvas.alpha -= Time.deltaTime;
                if (mycanvas.alpha == 0)
                {
                    fadeout = false;

                }
            }
        }
    }





}
