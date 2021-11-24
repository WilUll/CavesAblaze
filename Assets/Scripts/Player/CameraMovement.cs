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

    float yOffset;

    private Vector2 threshold;
    private Rigidbody2D rb;
    private PlayerMovement player;

    float xDifference;
    float yDifference;

    void Start()
    {
        threshold = calculateThreshold();
        rb = followObject.GetComponent<Rigidbody2D>();
        player = followObject.GetComponent<PlayerMovement>();

        yOffset = yOffsetPublic;
    }

    void FixedUpdate()
    {
        if (Input.GetAxis("Vertical") < 0)
        {
            yOffset = cameraLookDown;
        }
        else if (Input.GetAxis("Vertical") > 0)
        {
            yOffset = cameraLookUp;
        }
        else
        {
            yOffset = yOffsetPublic;
        }

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
    
        Vector3 calculateThreshold()
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
            Vector2 border = calculateThreshold();
            Gizmos.DrawWireCube(transform.position, new Vector3(border.x * 2, border.y * 2, 1));
        }
}