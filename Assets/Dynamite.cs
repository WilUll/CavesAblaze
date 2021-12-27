using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dynamite : MonoBehaviour
{
    LineRenderer LR;
    public Transform[] fusePos;
    public bool isIgnited;
    float startTime = 0;

    int index = 2;
    // Start is called before the first frame update
    void Start()
    {
        LR = GetComponent<LineRenderer>();
        LR.positionCount = fusePos.Length;
        for (int i = 0; i < fusePos.Length; i++)
        {
            LR.SetPosition(i, (fusePos[i].position));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isIgnited)
        {
            if (Vector2.Distance(fusePos[fusePos.Length - 1].position, fusePos[0].position) > 1f)
            {
                startTime += Time.deltaTime;
                float leftToGo = (startTime / 5);
                LR.SetPosition(index, Vector2.Lerp(fusePos[index].position, fusePos[index - 1].position, leftToGo));
                fusePos[index].position = LR.GetPosition(index);
                Debug.Log(leftToGo);
                if (leftToGo >= 1)
                {
                    startTime = 0;
                    index--;
                    Debug.Log(index);
                    if (index == 0)
                    {
                        index = fusePos.Length - 1;
                    }
                }

            }
            else
            {
                isIgnited = false;
                gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isIgnited)
        {
            isIgnited = true;
        }
    }

    void IgniteFuse()
    {

    }
}
