using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialText : MonoBehaviour
{
    TextMeshProUGUI tutText;
    public string[] sentences;
    int index;

    // Start is called before the first frame update
    void Start()
    {
        tutText = GetComponent<TextMeshProUGUI>();
        tutText.text = sentences[index].ToString();
        tutText.CrossFadeAlpha(1f, 0f, false);
        Debug.Log(Input.GetJoystickNames());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            StartCoroutine(fade());
        }
    }

    IEnumerator fade()
    {
        tutText.CrossFadeAlpha(0f, 0.2f, false);
        yield return new WaitForSeconds(0.2f);
        tutText.text = "";
        index++;
        if (index > sentences.Length)
        {

        }
        else
        {
            tutText.text = sentences[index].ToString();
            tutText.CrossFadeAlpha(1f, 0.2f, false);
        }
    }
}
