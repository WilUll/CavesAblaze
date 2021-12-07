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

    public float yMinClamp, yMaxClamp, xMinClamp, xMaxClamp, ortographicSize;

    float yOffset;

    private Vector2 threshold;
    private Rigidbody2D rb;
    private PlayerMovement player;

    float xDifference;
    float yDifference;

    void Start()
    {
        threshold = CalculateThreshold();
        rb = followObject.GetComponent<Rigidbody2D>();
        player = followObject.GetComponent<PlayerMovement>();

        yOffset = yOffsetPublic;
    }


    void FixedUpdate()
    {
        Vector2 follow = followObject.transform.position;

        xDifference = Vector2.Distance(Vector2.right * transform.position.x, Vector2.right * follow.x);

        yDifference = Vector2.Distance(Vector2.up * transform.position.y, Vector2.up * follow.y);

        Vector3 newPosition = transform.position;
        newPosition.x = Mathf.Clamp(transform.position.x, xMinClamp, xMaxClamp);
        newPosition.y = Mathf.Clamp(transform.position.y, yMinClamp, yMaxClamp);

        if (Mathf.Abs(xDifference) >= threshold.x)
        {
            newPosition.x = Mathf.Clamp(follow.x, xMinClamp, xMaxClamp);
        }
        if (Mathf.Abs(yDifference) >= threshold.y) 
        {
            newPosition.y = Mathf.Clamp(follow.y + yOffset, yMinClamp, yMaxClamp);
        }
        float moveSpeed = rb.velocity.magnitude > speed ? rb.velocity.magnitude : speed;

        transform.position = Vector3.Lerp(transform.position, newPosition, 5f * Time.deltaTime);

        //transform.LookAt(followObject.transform);
        transform.rotation = Quaternion.identity;

        Camera.main.orthographicSize = ortographicSize;
    }
    Vector3 CalculateThreshold()
    {
        Rect aspect = Camera.main.pixelRect;
        Vector2 t = new Vector2(Camera.main.orthographicSize * aspect.width / aspect.height, Camera.main.orthographicSize);
        t.x -= followOffset.x;
        t.y -= followOffset.y;
        return t;
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Vector2 border = CalculateThreshold();
        Gizmos.DrawWireCube(transform.position, new Vector3(border.x * 2, border.y * 2, 1));
    }
}