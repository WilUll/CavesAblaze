using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadingScreenManager : MonoBehaviour
{
    public TextMeshProUGUI time, score, deaths;

    private void Start()
    {
        deaths.text = SceneScript.Instance.playerDeaths.ToString();
        string minutes = Mathf.Floor(SceneScript.Instance.timer / 60).ToString("00");
        string seconds = (SceneScript.Instance.timer % 60).ToString("00");
        string milliseconds = ((SceneScript.Instance.timer * 1000) % 1000).ToString("000");
        time.text = string.Format("{0:0}:{1:00}:{2:000}", minutes, seconds, milliseconds);

        if (SceneScript.Instance.playerDeaths == 0)
        {
            int scoreInt = (int)((1000 / (SceneScript.Instance.timer))* 200);
            score.text = scoreInt.ToString();
        }
        else
        {
            int scoreInt = (int)((1000 / (SceneScript.Instance.timer * SceneScript.Instance.playerDeaths))*100);
            score.text = scoreInt.ToString();
        }
    }
}
