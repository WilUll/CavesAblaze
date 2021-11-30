using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMovement : MonoBehaviour
{
    public Renderer fireRend;

    // Start is called before the first frame update
    void Start()
    {
        fireRend.material.shader = Shader.Find("PlayerFire");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
