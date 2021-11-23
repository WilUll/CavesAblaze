using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mainmenu : MonoBehaviour
{
    public void Startgame()
    {
        SceneManager.LoadScene("Testgaming");
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
}
