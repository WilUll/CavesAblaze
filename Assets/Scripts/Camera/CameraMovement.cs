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
    public float maxPanoramicSize, progresiveCameraDistance, fixedCameraDistance;

    float yOffset;

    private Vector2 threshold;
    private Rigidbody2D rb;
    private PlayerMovement2 player;

    float xDifference;
    float yDifference;

    public bool panoramicCameraON, fixedCameraOn;

    void Start()
    {
        threshold = CalculateThreshold();
        rb = followObject.GetComponent<Rigidbody2D>();
        player = followObject.GetComponent<PlayerMovement2>();

        yOffset = yOffsetPublic;

        fixedCameraOn = true;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (panoramicCameraON)
            {
                panoramicCameraON = false;
                fixedCameraOn = true;
            }
            else if (fixedCameraOn)
            {
                panoramicCameraON = true;
                fixedCameraOn = false;
            }
        }

        PanoramicCamera();
    }

    void FixedUpdate()
    {
        //if (Input.GetAxis("Vertical") < 0)
        //{
        //    yOffset = cameraLookDown;
        //}
        //else if (Input.GetAxis("Vertical") > 0)
        //{
        //    yOffset = cameraLookUp;
        //}
        //else
        //{
        //    yOffset = yOffsetPublic;
        //}

        Vector2 follow = followObject.transform.position;

        xDifference = Vector2.Distance(Vector2.right * transform.position.x, Vector2.right * follow.x);

        yDifference = Vector2.Distance(Vector2.up * transform.position.y, Vector2.up * follow.y);

        Vector3 newPosition = transform.position;

        if (Mathf.Abs(xDifference) >= threshold.x)
        {
            newPosition.x = follow.x;
        }
        if (Mathf.Abs(yDifference) >= threshold.y) //&& player.isGrounded
        {
            newPosition.y = follow.y + yOffset;
        }
        float moveSpeed = rb.velocity.magnitude > speed ? rb.velocity.magnitude : speed;

        transform.position = Vector3.Lerp(transform.position, newPosition, 5f * Time.deltaTime);

        transform.LookAt(followObject.transform);
        transform.rotation = Quaternion.identity;
    }

    void PanoramicCamera()
    {
        if (panoramicCameraON)
        {
            Camera.main.orthographicSize += progresiveCameraDistance;
            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, fixedCameraDistance, maxPanoramicSize);
        }
        else if (fixedCameraOn)
        {
            Camera.main.orthographicSize = fixedCameraDistance;
        }
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