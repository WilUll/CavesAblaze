using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashCrystalLightController : MonoBehaviour
{
    UnityEngine.Experimental.Rendering.Universal.Light2D[] crystalLightsScripts;
    GameObject[] allLights;
    DashWallsDestroy dashWallsScript;

    public float intensityFluctuation;
    public float minIntensity, maxIntensity;

    public int index;

    void Start()
    {
        dashWallsScript = gameObject.GetComponent<DashWallsDestroy>();

        CheckChilds();
        SetAmountOfLights();
        GetChildrenLightObjects();

        SetMinimunIntensityToAllLights();
    }

    void Update()
    {
        index = dashWallsScript.indexNumber;

        ChangeIntensity();

        crystalLightsScripts[index].intensity += intensityFluctuation;

        if (index == 3)
        {
            crystalLightsScripts[index + 1].intensity += intensityFluctuation;
        }
    }
    
    private void CheckChilds()
    {
        foreach (Transform child in transform)
        {
            index++;
        }
    }
    private void SetAmountOfLights()
    {
        allLights = new GameObject[index];
        crystalLightsScripts = new UnityEngine.Experimental.Rendering.Universal.Light2D[index];
    }
    private void GetChildrenLightObjects()
    {
        for (int i = 0; i < allLights.Length; i++)
        {
            allLights[i] = gameObject.transform.Find("LightSprite" + i).gameObject;
            crystalLightsScripts[i] = allLights[i].GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>();
        }
    }
    private void SetMinimunIntensityToAllLights()
    {
        for (int i = 0; i < allLights.Length; i++)
        {
            crystalLightsScripts[i].intensity = minIntensity;
        }
    }
    
    private void ChangeIntensity()
    {
        if (crystalLightsScripts[index].intensity < minIntensity)
        {
            intensityFluctuation *= -1;
        }
        if (crystalLightsScripts[index].intensity > maxIntensity)
        {
            intensityFluctuation *= -1;
        }
    }
    public void HandleLights(int indexNumber)
    {
        switch (indexNumber)
        {
            case 0:
                allLights[0].SetActive(true);
                break;
            case 1:
                allLights[0].SetActive(false);
                allLights[1].SetActive(true);
                break;
            case 2:
                allLights[1].SetActive(false);
                allLights[2].SetActive(true);
                break;
            case 3:
                allLights[2].SetActive(false);
                allLights[3].SetActive(true);
                allLights[4].SetActive(true);
                break;
        }
    }
    
}
