using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform Target;

    void Update()
    {
        transform.position = new Vector3(Target.transform.position.x, Target.transform.position.y, -10);
    }
}
