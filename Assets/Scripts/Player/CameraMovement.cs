using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject followObject;
    public Vector2 followOffset;
    public float speed = 3f;
    public float yOffsetPublic;

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

    void Update()
    {
        if (Input.GetAxis("Vertical") < 0)
        {
            yOffset = -2;
        }
        else if (Input.GetAxis("Vertical") > 0)
        {
            yOffset = 5;
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
                newPosition.x = follow.x +2;
            }
            if (Mathf.Abs(yDifference) >= threshold.y && player.isGrounded)
            {
                newPosition.y = follow.y + yOffset;
            }
            float moveSpeed = rb.velocity.magnitude > speed ? rb.velocity.magnitude : speed;
            transform.position = Vector3.MoveTowards(transform.position, newPosition, moveSpeed * Time.deltaTime);
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