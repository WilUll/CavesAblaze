using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float yMin, yMax, xMin, xMax, targetOrthographicSize, progresiveCameraMovement;
    CameraMovement cameraScript;
    GameObject mainCam;

    PlayerMovement playerScript;
    GameObject player;

    public bool changeCamera, invertMovementValue, expandOrtSize, shrinkOrtSize;

    private void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("CameraParent");
        cameraScript = mainCam.GetComponent<CameraMovement>();

        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<PlayerMovement>();

        cameraScript.orthographicSize = targetOrthographicSize;

        invertMovementValue = true;
    }

    private void FixedUpdate()
    {
        ChangeCamera();
    }

    private void ChangeCamera()
    {
        if (changeCamera)
        {
            HandleZoomInConditions();
            HandleZoomOutConditions();

            MoveCameraProgressively();
            CheckMaximumZoomInValueAndStopChanging();
            CheckMaximumZoomOutValueAndStopChanging();
        }
    }

    private void CheckMaximumZoomInValueAndStopChanging()
    {
        if (cameraScript.orthographicSize <= targetOrthographicSize && shrinkOrtSize)
        {
            cameraScript.orthographicSize = targetOrthographicSize;
            changeCamera = false;
            shrinkOrtSize = false;
        }
    }
    private void CheckMaximumZoomOutValueAndStopChanging()
    {
        if (cameraScript.orthographicSize >= targetOrthographicSize && expandOrtSize)
        {
            cameraScript.orthographicSize = targetOrthographicSize;
            changeCamera = false;
            expandOrtSize = false;
        }
    }

    private void HandleZoomInConditions()
    {
        if (cameraScript.previousOrthograficSize > targetOrthographicSize)
        {
            if (progresiveCameraMovement > 0)
            {
                progresiveCameraMovement *= -1;
            }
            shrinkOrtSize = true;
        }
    }
    private void HandleZoomOutConditions()
    {
        if (cameraScript.previousOrthograficSize < targetOrthographicSize)
        {
            if (progresiveCameraMovement < 0)
            {
                progresiveCameraMovement *= -1;
            }
            expandOrtSize = true;
        }
    }
    private void MoveCameraProgressively()
    {
        if (expandOrtSize || shrinkOrtSize) cameraScript.orthographicSize += progresiveCameraMovement;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var script = this.GetComponent<CameraController>();
            script.enabled = true;

            cameraScript.yMinClamp = yMin;
            cameraScript.yMaxClamp = yMax;
            cameraScript.xMinClamp = xMin;
            cameraScript.xMaxClamp = xMax;

            changeCamera = true;
            invertMovementValue = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            cameraScript.previousOrthograficSize = targetOrthographicSize;

            var script = this.GetComponent<CameraController>();
            script.enabled = false;
        }
    }

}
