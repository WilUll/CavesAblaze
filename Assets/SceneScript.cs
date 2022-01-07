using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneScript : MonoBehaviour
{
    public static SceneScript Instance { get; private set; }
    public int index = 2;
    GameObject player;
    public int playerDeaths;
    public float timer;
    public bool stopTimer = false;
    public float totalScore;

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

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void LoadLoadingScreen()
    {
        StartCoroutine(fadeLoadScreen());
    }

    public void LoadMainScreen()
    {
        StartCoroutine(fadeMainScreen());
    }
    public void loadNextLevel()
    {
        stopTimer = false;
        SceneManager.LoadScene(index, LoadSceneMode.Single);
        timer = 0;
        playerDeaths = 0;

        Debug.Log(index);

        if (index == 8)
        {
            Debug.Log("2");
            DontDestroyManager.musicSingleton.PlayEndSceneMusic();
        }
    }

    IEnumerator fadeLoadScreen()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        FadeOnDeath fadeScript = playerObject.GetComponent<FadeOnDeath>();
        fadeScript.fade.CrossFadeAlpha(1, 0.15f, true);
        index = SceneManager.GetActiveScene().buildIndex;
        index++;
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(1, LoadSceneMode.Single);

    }

    IEnumerator fadeMainScreen()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        FadeOnDeath fadeScript = playerObject.GetComponent<FadeOnDeath>();
        fadeScript.fade.CrossFadeAlpha(1, 0.15f, true);
        index = SceneManager.GetActiveScene().buildIndex;
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene("Mainmenu");
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
    private void Update()
    {
        if (!stopTimer && player != null)
        {
            timer += Time.deltaTime;
        }
        else if (player == null && SceneManager.GetActiveScene().name != "LoadingScreen")
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }
}
