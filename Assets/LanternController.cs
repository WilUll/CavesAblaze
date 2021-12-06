using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternController : MonoBehaviour
{
    
    

    public float rotation1, rotation2, rotation3;

    float[] lanterPositions = new float[4];

    int currentRotation, initialRotation;

    // Start is called before the first frame update
    void Start()
    {
        lanterPositions[0] = initialRotation;
        lanterPositions[1] = rotation1;
        lanterPositions[2] = rotation2;
        lanterPositions[3] = rotation3;

        transform.rotation = Quaternion.Euler(0, 0, lanterPositions[currentRotation]);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            currentRotation++;

            if (currentRotation > lanterPositions.Length) currentRotation = initialRotation;

            transform.rotation = Quaternion.Euler(0, 0, lanterPositions[currentRotation]);
        }
    }
}
