using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TotalScoreEndSceneManager : MonoBehaviour
{
    public TextMeshProUGUI text;

    void Start()
    {
        text.text = "Total Score: " + SceneScript.Instance.totalScore.ToString();
    }
}
