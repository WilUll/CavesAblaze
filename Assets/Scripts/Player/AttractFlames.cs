using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttractFlames : MonoBehaviour
{
    public Transform target;

    public float speed = 100;
    public Vector3 velocity = Vector3.zero;

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("jumpFlames"))
        {
            other.transform.position = Vector3.SmoothDamp(other.transform.position, target.position, ref velocity, speed * Time.deltaTime);
        }
    }
}
