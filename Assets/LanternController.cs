using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternController : MonoBehaviour
{
    
    

    public float initialRotation, maxRotation;

    float currentRotation;

    public float inputMovementSpeed, goBackSpeed;

    bool rotate;

    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.B))
        {
            rotate = true;
        }
        else
        {
            rotate = false;
        }

        if (rotate)
        {
            if (currentRotation > maxRotation) currentRotation -= inputMovementSpeed;
            else currentRotation = initialRotation;
        }
        else
        {
            if (currentRotation > initialRotation) currentRotation = initialRotation;
            if(currentRotation != initialRotation) currentRotation += goBackSpeed;
        }

        
        transform.rotation = Quaternion.Euler(0, 0, currentRotation);
        

    }
}
