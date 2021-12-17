using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingPlayerAnimation : MonoBehaviour
{
    float[] rotations = new float [7];

    public float resetRotationTimer;
    public Vector2 yOffsetMovement;

    float changeRotationTimer;

    int index = 0;



    void Start()
    {
        changeRotationTimer = resetRotationTimer;

        rotations[0] = -20;
        rotations[1] = -10;
        rotations[2] = 0;
        rotations[3] = 10;
        rotations[4] = 20;
        rotations[5] = 10;
        rotations[6] = 0;
        rotations[7] = -10;
    }


    void Update()
    {
        if (changeRotationTimer <= 0)
        {
            index++;

            if (index > 7)
            {
                index = 0;
            }

            transform.rotation = Quaternion.Euler(0, 0, rotations[index]);
            transform.localPosition = yOffsetMovement;

            changeRotationTimer = resetRotationTimer;
        }
        else
        {
            transform.localPosition = Vector2.zero;
            changeRotationTimer -= Time.deltaTime;
        }

    }
}
