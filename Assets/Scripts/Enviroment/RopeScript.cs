using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeScript : MonoBehaviour
{
    public Transform ropeHangPoint;
    public Transform ropeStopPoint;

    LineRenderer LineRenderer;


    // Start is called before the first frame update
    void Start()
    {
        LineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
