using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Screenshot : MonoBehaviour
{
    public int multiplier = 1;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            string date = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            string name = Application.productName + date + ".png";
            ScreenCapture.CaptureScreenshot(name, multiplier);
            Debug.Log("Screenshot saved to: " + name);
        }
    }
}
