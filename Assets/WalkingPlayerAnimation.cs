using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingPlayerAnimation : MonoBehaviour
{
    float[] rotations = {-20, -10, 0, 10, 20, 10, 0, -10};

    public float resetRotationTimer;
    public Vector3 yOffsetMovement;

    float changeRotationTimer;

    int index = 0;

    int loopTimes;

    void Start()
    {
        changeRotationTimer = resetRotationTimer;
    }


    void Update()
    {
        if (changeRotationTimer <= 0)
        {
            index++;

            if (index >= rotations.Length)
            {
                index = 0;
            }

            transform.rotation = Quaternion.Euler(0, 0, rotations[index]);
            transform.localPosition += (yOffsetMovement);

            changeRotationTimer = resetRotationTimer;

            loopTimes++;
            if (loopTimes == 3)
            {
                yOffsetMovement = -yOffsetMovement;
                loopTimes = 0;
            }
        }
        else
        {
            changeRotationTimer -= Time.deltaTime;
        }
    }
}
