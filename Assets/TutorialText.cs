using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialText : MonoBehaviour
{
    TextMeshProUGUI tutText;
    public string[] pcSentences;
    public string[] controllerSentences;
    int index;
    public string[] sentences;
    bool isUsingController;
    PlayerMovement playerScript;
    DashController playerDashScript;
    public bool move = false, dash = false, jump = false;

    // Start is called before the first frame update
    void Start()
    {
        move = true;
        tutText = GetComponent<TextMeshProUGUI>();
        tutText.CrossFadeAlpha(1f, 0f, false);
        Debug.Log(Input.GetJoystickNames());

        for (int i = 0; i < Input.GetJoystickNames().Length; i++)
        {
            if (Input.GetJoystickNames()[i] == "")
            {

            }
            else
            {
                isUsingController = true;
            }
        }

        if (isUsingController)
        {
            sentences = new string[controllerSentences.Length];
            for (int i = 0; i < controllerSentences.Length; i++)
            {
                sentences[i] = controllerSentences[i];
            }
        }
        else
        {
            sentences = new string[pcSentences.Length];
            for (int i = 0; i < controllerSentences.Length; i++)
            {
                sentences[i] = pcSentences[i];
            }
        }
        tutText.text = sentences[index].ToString();
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        playerDashScript = GameObject.FindGameObjectWithTag("Player").GetComponent<DashController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (move && playerScript.xAxis < 0 || playerScript.xAxis > 0 && move)
        {
            move = false;
            dash = true;
            StartCoroutine(fade());
        }
        else if (dash && playerDashScript.dashOn)
        {
            dash = false;
            jump = true;
            StartCoroutine(fade());
        }
        else if (jump && playerScript.jumping)
        {
            jump = false;
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
