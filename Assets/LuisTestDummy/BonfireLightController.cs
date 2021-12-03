using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonfireLightController : MonoBehaviour
{
    UnityEngine.Experimental.Rendering.Universal.Light2D Bonfire;

    public float intensityFluctuation, outteRadiusFluctuation;

    public float minIntensity, maxIntensity, minOutterRadius, maxOutterRadius;


    void Start()
    {
        Bonfire = GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>();
        Bonfire.intensity = minIntensity;
        Bonfire.pointLightOuterRadius = minOutterRadius;
    }

    private void Update()
    {
        ChangeIntensity();
        ChangeOutterRadius();

        Bonfire.intensity += intensityFluctuation;
        Bonfire.pointLightOuterRadius += outteRadiusFluctuation;
    }

    private void ChangeIntensity()
    {
        if (Bonfire.intensity < minIntensity)
        {
            intensityFluctuation *= -1;
        }
        if (Bonfire.intensity > maxIntensity)
        {
            intensityFluctuation *= -1;
        }
    }
    private void ChangeOutterRadius()
    {
        if (Bonfire.pointLightOuterRadius < minOutterRadius)
        {
            outteRadiusFluctuation *= -1;
        }
        if (Bonfire.pointLightOuterRadius > maxOutterRadius)
        {
            outteRadiusFluctuation *= -1;
        }
    }
}
