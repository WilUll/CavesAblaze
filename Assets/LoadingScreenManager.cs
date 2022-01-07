using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadingScreenManager : MonoBehaviour
{
    public TextMeshProUGUI time, score, deaths;
    float finalScore, x, timer;
    public bool added,stop = true;

    private void Start()
    {
        x = 0;
        score.text = 0.ToString();
        deaths.text = SceneScript.Instance.playerDeaths.ToString();
        string minutes = Mathf.Floor(SceneScript.Instance.timer / 60).ToString("00");
        string seconds = Mathf.FloorToInt((SceneScript.Instance.timer % 60)).ToString();
        string milliseconds = ((SceneScript.Instance.timer * 1000) % 1000).ToString("000");
        time.text = string.Format("{0:0}:{1:00}:{2:000}", minutes, seconds, milliseconds);

        if (SceneScript.Instance.playerDeaths == 0)
        {
            int scoreInt = (int)((1000 / (SceneScript.Instance.timer))* 200);
            scoreInt = (Mathf.RoundToInt(scoreInt / 100)) * 100;
            finalScore = scoreInt;
            stop = true;
        }
        else
        {
            int scoreInt = (int)((1000 / (SceneScript.Instance.timer * SceneScript.Instance.playerDeaths))*100);
            scoreInt = (Mathf.RoundToInt(scoreInt / 100)) * 100;
            finalScore = scoreInt;
            stop = true;
        }
        

    }
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 0.25f)
        {
            stop = false;
        }
        if (!stop)
        {
            x +=(finalScore - x) * 0.01f;
            x = Mathf.RoundToInt(x);
            score.text = x.ToString();
        }
        if (x + 250 >= finalScore && !stop)
        {
            stop = true;
            x = finalScore;
            if (added)
            {

            }
            else
            {
                SceneScript.Instance.totalScore += finalScore;
                added = true;
            }
            score.text = x.ToString();
        }

    }
}
