using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLightController : MonoBehaviour
{
    UnityEngine.Experimental.Rendering.Universal.Light2D Player;

    public float intensityFluctuation, outteRadiusFluctuation;

    public float minIntensity, maxIntensity, minOutterRadius, maxOutterRadius;

    float randomValueIntensity, randomValueOutterRadius;

    void Start()
    {
        Player = GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>();
        Player.intensity = minIntensity;
        Player.pointLightOuterRadius = minOutterRadius;

    }

    private void Update()
    {
        //randomValueIntensity = Random.Range(minIntensity, maxIntensity);
        //randomValueOutterRadius = Random.Range(minIntensity, maxIntensity);

        ChangeIntensity();
        ChangeOutterRadius();

        //Player.intensity = randomValueIntensity;
        //Player.pointLightOuterRadius = randomValueOutterRadius;

        Player.intensity += intensityFluctuation;
        Player.pointLightOuterRadius += outteRadiusFluctuation;
    }

    private void ChangeIntensity()
    {
        if (Player.intensity < minIntensity)
        {
            intensityFluctuation *= -1;
        }
        if (Player.intensity > maxIntensity)
        {
            intensityFluctuation *= -1;
        }
    }
    private void ChangeOutterRadius()
    {
        if (Player.pointLightOuterRadius < minOutterRadius)
        {
            outteRadiusFluctuation *= -1;
        }
        if (Player.pointLightOuterRadius > maxOutterRadius)
        {
            outteRadiusFluctuation *= -1;
        }
    }
}
