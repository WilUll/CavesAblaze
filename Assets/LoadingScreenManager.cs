using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadingScreenManager : MonoBehaviour
{
    public TextMeshProUGUI time, score, deaths;
    float finalScore, x;
    bool added,stop = true;

    private void Start()
    {
        x = 0;
        deaths.text = SceneScript.Instance.playerDeaths.ToString();
        string minutes = Mathf.Floor(SceneScript.Instance.timer / 60).ToString("00");
        string seconds = (SceneScript.Instance.timer % 60).ToString("00");
        string milliseconds = ((SceneScript.Instance.timer * 1000) % 1000).ToString("000");
        time.text = string.Format("{0:0}:{1:00}:{2:000}", minutes, seconds, milliseconds);

        if (SceneScript.Instance.playerDeaths == 0)
        {
            int scoreInt = (int)((1000 / (SceneScript.Instance.timer))* 200);
            scoreInt = (Mathf.RoundToInt(scoreInt / 100)) * 100;
            finalScore = scoreInt;
            stop = false;
        }
        else
        {
            int scoreInt = (int)((1000 / (SceneScript.Instance.timer * SceneScript.Instance.playerDeaths))*100);
            scoreInt = (Mathf.RoundToInt(scoreInt / 100)) * 100;
            finalScore = scoreInt;
            stop = false;
        }
        if (added)
        {

        }
        else
        {
            SceneScript.Instance.totalScore += finalScore;
            added = true;
        }

    }
    private void Update()
    {
        if (!stop)
        {
            x +=(finalScore - x) * 0.005f;
            x = Mathf.RoundToInt(x);
        }
        else if (x + 1 >= finalScore)
        {
            stop = true;
            x = finalScore;
        }
        score.text = x.ToString();
    }
}
