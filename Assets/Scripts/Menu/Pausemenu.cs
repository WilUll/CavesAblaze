using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pausemenu : MonoBehaviour
{

    public static bool Gamepause = false;

    public GameObject pausemenuui;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Gamepause)
            {
                Resume();
            }
            else
            {
                Pause();
            }

        }
    }


    void Resume()
    {
        pausemenuui.SetActive(false);
        Time.timeScale = 1f;
        Gamepause = false;
    }

    void Pause()
    {
        pausemenuui.SetActive(true);
        Time.timeScale = 0f;
        Gamepause = true;
    }
}
