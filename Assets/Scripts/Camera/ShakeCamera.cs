using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    Camera mainCamera;

    float shakeAmount = 0, startTime;

    bool stopShaking;

    private void Awake()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Shake(0.3f, 0.4f);
        }

        if (stopShaking)
        {
            startTime += Time.deltaTime;
            float leftToGo = (startTime / 10);
            transform.localPosition = Vector3.Lerp(transform.localPosition, Vector3.zero, leftToGo);

            if (transform.localPosition == Vector3.zero)
            {
                startTime = 0;
                stopShaking = false;
            }
            if (startTime == 0) transform.localPosition = Vector3.zero;

        }

    }

    public void Shake(float amount, float lenght)
    {
        shakeAmount = amount;
        InvokeRepeating("StartShake", 0, 0.01f);
        Invoke("StopShake", lenght);
    }

    void StartShake()
    {
        if (shakeAmount > 0)
        {
            Vector3 cameraPosition = mainCamera.transform.position;

            float offsetX = Random.value * shakeAmount * 2 - shakeAmount;
            float offsetY = Random.value * shakeAmount * 2 - shakeAmount;

            cameraPosition.x += offsetX;
            cameraPosition.y += offsetY;

            mainCamera.transform.position = cameraPosition;

            stopShaking = false;
        }
    }

    void StopShake()
    {
        CancelInvoke("StartShake");

        stopShaking = true;
    }
}
