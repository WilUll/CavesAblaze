using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class batanimcooldown : MonoBehaviour
{

    public Animation batflying;
    public bool keepplaying;
    public int stop = 20;

    
    void Start()
    {
        batflying = GetComponent<Animation>();
        keepplaying = true;
        StartCoroutine(routine());
    }

    private IEnumerator routine()
    {
        int loop = 0;
        while (keepplaying == true)
        {
            batflying.Play("BatFlying");
            yield return new WaitForSeconds(5);
            batflying.Stop("BatFlying");
            loop++;
            if (loop >= stop)
            {
                yield break;
            }
        }

        yield return null;
    }

}
