using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadButtonScriptFix : MonoBehaviour
{
    public Button loadButton;
    public int button;
    // Start is called before the first frame update
    void Start()
    {
        if (button == 0)
        { 
        loadButton.onClick.AddListener(SceneScript.Instance.loadNextLevel);
        }
        if (button == 1)
        {
            loadButton.onClick.AddListener(SceneScript.Instance.LoadMainScreen);
        }
        if (button == 2)
        {
            loadButton.onClick.AddListener(SceneScript.Instance.loadNextLevel);
        }
        if (button == 3)
        {
            loadButton.onClick.AddListener(SceneScript.Instance.Quitgame);
        }
    }
}
