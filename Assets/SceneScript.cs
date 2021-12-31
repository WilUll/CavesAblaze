using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneScript : MonoBehaviour
{
    public static SceneScript Instance { get; private set; }
    int index;
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
        StartCoroutine(fadeNextLevel());
    }

    IEnumerator fadeNextLevel()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        FadeOnDeath fadeScript = playerObject.GetComponent<FadeOnDeath>();
        index = SceneManager.GetActiveScene().buildIndex;
        index++;
        fadeScript.fade.CrossFadeAlpha(1, 0.25f, true);
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(index, LoadSceneMode.Single);
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
