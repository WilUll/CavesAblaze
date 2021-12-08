using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneScript : MonoBehaviour
{
    public static SceneScript Instance { get; private set; }
    int index = 2;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(index, LoadSceneMode.Single);
        index++;
    }
    public void Gotosettings()
    {
        SceneManager.LoadScene("Settings");
    }

    public void Gotomain()
    {
        SceneManager.LoadScene("Mainmenu");
    }

    public void Quitgame()
    {
        Application.Quit();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            LoadLevel();
        }
    }
}
