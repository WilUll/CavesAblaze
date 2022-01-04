using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneScript : MonoBehaviour
{
    public static SceneScript Instance { get; private set; }
    int index;
    GameObject player;
    public int playerDeaths;
    public float timer;
    public bool stopTimer = false;

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

    public void loadNextLevel()
    {
        SceneManager.LoadScene(index, LoadSceneMode.Single);
        timer = 0;
        stopTimer = false;
        playerDeaths = 0;
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
        if (player != null && !stopTimer)
        {
            timer += Time.deltaTime;
        }
        else if (player == null && SceneManager.GetActiveScene().name != "LoadingScreen")
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }
}
