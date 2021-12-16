using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    Camera mainCamera;

    float shakeAmount = 0;

    private void Awake()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            Shake(1f, 0.2f);
        }
    }

    public void Shake (float amount, float lenght)
    {
        shakeAmount = amount;
        InvokeRepeating("StartShake", 0, 0.1f);
        Invoke("StopShake", lenght);
    }

    void StartShake()
    {
        if(shakeAmount > 0)
        {
            Vector3 cameraPosition = mainCamera.transform.position;

            float offsetX = Random.value * shakeAmount * 2 - shakeAmount;
            float offsetY = Random.value * shakeAmount * 2 - shakeAmount;

            cameraPosition.x += offsetX;
            cameraPosition.y += offsetY;

            mainCamera.transform.position = cameraPosition;
        }
    }

    void StopShake ()
    {
        CancelInvoke("StartShake");

        mainCamera.transform.localPosition = Vector3.zero;
    }
}
