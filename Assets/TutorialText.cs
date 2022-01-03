using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialText : MonoBehaviour
{
    TextMeshProUGUI tutText;
    public string[] sentences;

    // Start is called before the first frame update
    void Start()
    {
        tutText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.G))
        //{
        //    tutText.text = "";
        //    for (int i = 0; i < triggerGameObject.Length; i++)
        //    {
        //        tutScript = triggerGameObject[i].GetComponent<TutorialTriggers>();
        //    }
        //}
    }
}
