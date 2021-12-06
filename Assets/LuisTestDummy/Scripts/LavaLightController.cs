using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaLightController : MonoBehaviour
{
    UnityEngine.Experimental.Rendering.Universal.Light2D Lava;

    public float intensityFluctuation, outteRadiusFluctuation;

    public float minIntensity, maxIntensity, minOutterRadius, maxOutterRadius;


    void Start()
    {
        Lava = GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>();
        Lava.intensity = minIntensity;
        Lava.pointLightOuterRadius = minOutterRadius;
    }

    private void Update()
    {
        ChangeIntensity();
        ChangeOutterRadius();

        Lava.intensity += intensityFluctuation;
        Lava.pointLightOuterRadius += outteRadiusFluctuation;
    }

    private void ChangeIntensity()
    {
        if (Lava.intensity < minIntensity)
        {
            intensityFluctuation *= -1;
        }
        if (Lava.intensity > maxIntensity)
        {
            intensityFluctuation *= -1;
        }
    }private void ChangeOutterRadius()
    {
        if (Lava.pointLightOuterRadius < minOutterRadius)
        {
            outteRadiusFluctuation *= -1;
        }
        if (Lava.pointLightOuterRadius > maxOutterRadius)
        {
            outteRadiusFluctuation *= -1;
        }
    }
}
