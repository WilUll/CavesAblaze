using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Hoverbutton : MonoBehaviour
{

    public RectTransform playbutton;


    public void OnPointerEnter(PointerEventData eventData)
    {
        playbutton.GetComponent<Animator>().Play("Hoverbutton");
        Debug.Log("You touched the button");
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        playbutton.GetComponent<Animator>().Play("Hoveroffbutton");
    }

}
