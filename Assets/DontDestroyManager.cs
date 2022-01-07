using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyManager : MonoBehaviour
{
    public static DontDestroyManager musicSingleton { get; private set; }

    public AudioSource audioSource;
    public AudioClip[] clip;



    private void Awake()
    {
        if (musicSingleton == null)
        {
            musicSingleton = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayEndSceneMusic()
    {
        Debug.Log("3");
        audioSource.clip =clip[0];
        audioSource.Play();

        Debug.Log("4");
    }

}
