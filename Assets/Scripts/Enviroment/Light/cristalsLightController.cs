using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cristalsLightController : MonoBehaviour
{
    UnityEngine.Experimental.Rendering.Universal.Light2D Cristal;

    public float intensityFluctuation, outteRadiusFluctuation;

    public float minIntensity, maxIntensity, minOutterRadius, maxOutterRadius;


    void Start()
    {
        Cristal = GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>();
        Cristal.intensity = minIntensity;
        Cristal.pointLightOuterRadius = minOutterRadius;
    }

    private void Update()
    {
        ChangeIntensity();
        ChangeOutterRadius();

        Cristal.intensity += intensityFluctuation;
        Cristal.pointLightOuterRadius += outteRadiusFluctuation;
    }

    private void ChangeIntensity()
    {
        if (Cristal.intensity < minIntensity)
        {
            intensityFluctuation *= -1;
        }
        if (Cristal.intensity > maxIntensity)
        {
            intensityFluctuation *= -1;
        }
    }
    private void ChangeOutterRadius()
    {
        if (Cristal.pointLightOuterRadius < minOutterRadius)
        {
            outteRadiusFluctuation *= -1;
        }
        if (Cristal.pointLightOuterRadius > maxOutterRadius)
        {
            outteRadiusFluctuation *= -1;
        }
    }
}
