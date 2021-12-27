using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeScript : MonoBehaviour
{
    public Rigidbody2D hook;
    public GameObject segment;
    public int numSegments;


    // Start is called before the first frame update
    void Start()
    {
        GenerateRope();
        StartCoroutine(timer());
    }

    // Update is called once per frame
    public void GenerateRope()
    {
        Rigidbody2D prevBod = hook;
        for (int i = 0; i < numSegments; i++)
        {
            GameObject newSeg = Instantiate(segment);
            newSeg.transform.parent = transform;
            newSeg.transform.position = transform.position;
            HingeJoint2D hj = newSeg.GetComponent<HingeJoint2D>();
            hj.connectedBody = prevBod;

            prevBod = newSeg.GetComponent<Rigidbody2D>();
        }
    }
    IEnumerator timer()
    {
        yield return new WaitForSeconds(1.5f);
        GameObject.FindGameObjectWithTag("Checkpoint").GetComponent<CheckpointSystem>().ResetRope();
    }

}
