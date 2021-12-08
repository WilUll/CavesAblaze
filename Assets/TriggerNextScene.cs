using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerNextScene : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SceneScript.Instance.LoadLevel();
    }

}
