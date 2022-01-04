using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyBatEndScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 2.1f);
    }
}
