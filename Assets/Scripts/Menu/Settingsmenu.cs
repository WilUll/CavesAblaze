using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class Settingsmenu : MonoBehaviour
{

    public AudioMixer audiomixer;

    public List<ResItems> resolutions = new List<ResItems>();
    int selectedRes;
    bool fullscreen;

    public TMP_Text resText;

    private void Start()
    {
        bool foundRes = false;
        for (int i = 0; i < resolutions.Count; i++)
        {
            if (Screen.width == resolutions[i].horizontal && Screen.height == resolutions[i].vertical)
            {
                foundRes = true;

                selectedRes = i;

                UpdateResText();
            }
        }
    }

    public void SetMasterVolume (float Master)
    {
        audiomixer.SetFloat("master", Master);
    }
    public void SetMusicVolume(float Music)
    {
        audiomixer.SetFloat("music", Music);
    }
    public void SetFxVolume(float fx)
    {
        audiomixer.SetFloat("fx", fx);
    }

    public void Setfullscreen(bool isfullscreen)
    {
        fullscreen = isfullscreen;
    }

    public void ResLeft()
    {
        selectedRes--;
        if (selectedRes < 0)
        {
            selectedRes = resolutions.Count-1;
        }
        UpdateResText();
    }

    public void ResRight()
    {
        selectedRes++;
        if (selectedRes > resolutions.Count - 1)
        {
            selectedRes = 0;
        }
        UpdateResText();
    }

    public void UpdateResText()
    {
        resText.text = resolutions[selectedRes].horizontal.ToString() + " x " + resolutions[selectedRes].vertical.ToString();
    }

    public void ApplyGraphics()
    {
        Screen.SetResolution(resolutions[selectedRes].horizontal, resolutions[selectedRes].vertical, fullscreen);
    }
}



[System.Serializable]
public class ResItems
{
    public int horizontal, vertical;
}