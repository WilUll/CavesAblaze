using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pausemenu : MonoBehaviour
{

    public static bool Gamepause = false;

    public GameObject Pausemenureal;

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


    public void Resume()
    {
        Pausemenureal.SetActive(false);
        Time.timeScale = 1f;
        Gamepause = false;
    }

    public void Pause()
    {
        Pausemenureal.SetActive(true);
        Time.timeScale = 0f;
        Gamepause = true;
    }


}
