using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyManager : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip [] clip;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        DontDestroyOnLoad(this.gameObject);

        if(SceneManager.GetActiveScene().name == "End scen")
        {
            audioSource.clip = clip[0];
            audioSource.Play();
        }
    }
}
