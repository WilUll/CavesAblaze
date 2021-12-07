using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float yMin, yMax, xMin, xMax, ortographicSize;
    CameraMovement cameraScript;
    GameObject mainCam;

    private void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("CameraParent");
        cameraScript = mainCam.GetComponent<CameraMovement>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        cameraScript.yMinClamp = yMin;
        cameraScript.yMaxClamp = yMax;
        cameraScript.xMinClamp = xMin;
        cameraScript.xMaxClamp = xMax;

        cameraScript.ortographicSize = ortographicSize;
    }

}
