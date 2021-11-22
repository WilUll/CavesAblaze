using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMovement : MonoBehaviour
{
    float[] xPositions;
    float[] yPositions;
    float[] velocities;
    float[] accelerations;
    LineRenderer LineRenderer;

    GameObject[] meshObjects;
    Mesh[] meshes;

    const float springConstant = 0.02f;
    const float damping = 0.04f;
    const float spread = 0.05f;
    const float z = -1f;

    float baseHeight;
    float left;
    float right;

    public GameObject splash;
    public Material mat;
    public GameObject waterMesh;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnWater(float left, float width, float top, float bottom)
    {

    }
}
