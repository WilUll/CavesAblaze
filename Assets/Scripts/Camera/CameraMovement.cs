using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject followObject;
    public Vector2 followOffset;
    public float speed = 3f;
    public float yOffsetPublic;

    public float cameraLookDown;
    public float cameraLookUp;

    public float yMinClamp, yMaxClamp, xMinClamp, xMaxClamp, orthographicSize, previousOrthograficSize;

    float yOffset, moveSpeed;

    private Vector2 threshold;
    private Vector3 newPosition;
    private Rigidbody2D rigidBody;

    Vector2 followedObjectPosition;

    float xDistance;
    float yDistance;

    void Start()
    {
        threshold = CalculateThreshold();

        rigidBody = followObject.GetComponent<Rigidbody2D>();

        yOffset = yOffsetPublic;
    }

    void FixedUpdate()
    {
        followedObjectPosition = followObject.transform.position;

        CalculateFollowedObejctWithCameraDistance(followedObjectPosition);

        newPosition = transform.position;

        DefineNewPositionXValue(followedObjectPosition);
        DefineNewPositionYValue(followedObjectPosition);

        RefreshCameraSpeed();
        RefreshCameraPositionOrMovement();
        RefreshCameraOrthographicSize();

        //keeps rotation fixed just in case
        transform.rotation = Quaternion.identity;


    }

    Vector3 CalculateThreshold()
    {
        Rect aspect = Camera.main.pixelRect;
        Vector2 threshold = new Vector2(Camera.main.orthographicSize * aspect.width / aspect.height, Camera.main.orthographicSize);
        threshold.x -= followOffset.x;
        threshold.y -= followOffset.y;
        return threshold;
    }
    private void CalculateFollowedObejctWithCameraDistance(Vector2 followedObjectPosition)
    {
        xDistance = Vector2.Distance(Vector2.right * transform.position.x, Vector2.right * followedObjectPosition.x);
        yDistance = Vector2.Distance(Vector2.up * transform.position.y, Vector2.up * followedObjectPosition.y);
    }
    private void DefineNewPositionXValue(Vector2 followedObjectPosition)
    {
        if (Mathf.Abs(xDistance) >= threshold.x)
        {
            newPosition.x = Mathf.Clamp(followedObjectPosition.x, xMinClamp, xMaxClamp);
        }
        else
        {
            newPosition.x = Mathf.Clamp(transform.position.x, xMinClamp, xMaxClamp);
        }
    }
    private void DefineNewPositionYValue(Vector2 followedObjectPosition)
    {
        if (Mathf.Abs(yDistance) >= threshold.y)
        {
            newPosition.y = Mathf.Clamp(followedObjectPosition.y + yOffset, yMinClamp, yMaxClamp);
        }
        else
        {
            newPosition.y = Mathf.Clamp(transform.position.y, yMinClamp, yMaxClamp);
        }
    }
    private void RefreshCameraOrthographicSize()
    {
        Camera.main.orthographicSize = orthographicSize;
    }
    private void RefreshCameraSpeed()
    {
        moveSpeed = rigidBody.velocity.magnitude > speed ? rigidBody.velocity.magnitude : speed;
    }
    private void RefreshCameraPositionOrMovement()
    {
        transform.position = Vector3.Lerp(transform.position, newPosition, moveSpeed * Time.deltaTime);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Vector2 border = CalculateThreshold();
        Gizmos.DrawWireCube(transform.position, new Vector3(border.x * 2, border.y * 2, 1));
    }
}