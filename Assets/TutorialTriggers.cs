using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTriggers : MonoBehaviour
{
    public GameObject HelpFly;
    public int tutIndex;
    public bool inTutTrigger;
    public flyScript helpFlyScript;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            inTutTrigger = true;
            helpFlyScript.checkWhatTrigger = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            inTutTrigger = false;
        }
    }

}
