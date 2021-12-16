using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class flyScript : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public string[] sentences;
    public float typingSpeed;

    public bool checkWhatTrigger = false;
    public bool inPos = false;
    public bool gotPos = false;
    public bool isTyping = false;
    public bool gotScript = false;


    Vector2 playerPos;

    GameObject[] triggerGameObject;
    GameObject player;

    TutorialTriggers tutScript;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        triggerGameObject = GameObject.FindGameObjectsWithTag("TutorialTriggers");
    }

    private void Update()
    {
        if (checkWhatTrigger)
        {
            for (int i = 0; i < triggerGameObject.Length; i++)
            {
                tutScript = triggerGameObject[i].GetComponent<TutorialTriggers>();
                if (tutScript.inTutTrigger)
                {
                    gotScript = true;
                }
            }
        }
        if (gotScript && !gotPos)
        {
            playerPos = player.transform.position;
            gotPos = true;
        }
        else if (gotPos)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerPos, 0.5f);
            if (Vector2.Distance(transform.position, playerPos + Vector2.up * 4) < 5)
            {
                inPos = true;
            }
        }

        if (inPos && !isTyping)
        {
            StartCoroutine(Type(tutScript.tutIndex));
        }
    }

    IEnumerator Type(int index)
    {
        isTyping = true;
        foreach (char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            if (letter == ' ')
            {
                yield return new WaitForSeconds(0);
            }
            else
            {
                yield return new WaitForSeconds(typingSpeed);
            }
        }

        tutScript.gameObject.SetActive(false);
        isTyping = false;
        gotScript = false;
        gotPos = false;
        inPos = false;

    }
}
