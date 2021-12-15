using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPicker : MonoBehaviour
{
    GameObject[] allLights;

    int index;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
        {
            index++;
        }

        allLights = new GameObject[index];

        for (int i = 0; i < allLights.Length; i++)
        {
            allLights[i] = gameObject.transform.Find("LightSprite" + i).gameObject;
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
