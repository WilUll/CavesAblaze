using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadButtonScriptFix : MonoBehaviour
{
    public Button loadButton;
    // Start is called before the first frame update
    void Start()
    {
        loadButton.onClick.AddListener(SceneScript.Instance.loadNextLevel);
    }
}
