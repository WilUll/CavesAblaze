using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mainmenu : MonoBehaviour
{
    public void Startgame()
    {
        SceneManager.LoadScene(2);
    }

    public void Gotosettings()
    {
        SceneManager.LoadScene(3);
    }

    public void Gotomain()
    {
        SceneManager.LoadScene(1);
    }

    public void Quitgame()
    {
        Application.Quit();
    }
}
