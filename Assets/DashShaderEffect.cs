using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashShaderEffect : MonoBehaviour
{
    Material dash;
    DashController playerScript;

    public float initialXShaderValue;
    float xShaderScale;

    void Start()
    {
        dash = GetComponent<SpriteRenderer>().material;
        playerScript = GetComponent<DashController>();

        xShaderScale = initialXShaderValue;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerScript.dashOn)
        {
            xShaderScale += 50;

            dash.SetFloat("_NoiseXScale", xShaderScale);

            dash.SetFloat("_NoiseScale", xShaderScale);
        }
        else if (!playerScript.dashOn)
        {
            xShaderScale = initialXShaderValue;

            dash.SetFloat("_NoiseXScale", xShaderScale);
        }
    }
}
