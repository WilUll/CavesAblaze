using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundValueLoader : MonoBehaviour
{
    public void LoadValues()
    {
        gameObject.transform.GetChild(0).GetComponent<Slider>().value = SceneScript.Instance.master;
        gameObject.transform.GetChild(1).GetComponent<Slider>().value = SceneScript.Instance.music;
        gameObject.transform.GetChild(2).GetComponent<Slider>().value = SceneScript.Instance.fx;
    }
}
