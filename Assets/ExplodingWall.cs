using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingWall : MonoBehaviour
{
    public int health;
    GameObject shatter;
    ParticleSystem shatterSystem;
    private void Start()
    {
        shatter = gameObject.transform.Find("ParticlesShatter").gameObject;
        shatterSystem = shatter.GetComponent<ParticleSystem>();
    }
    public void DamageWall(Transform positionSystem)
    {
        health--;
        shatter.transform.position = new Vector3(positionSystem.position.x - 0.5f, positionSystem.position.y, positionSystem.position.z);
        shatterSystem.Play();
        if (health == 0)
        {
            gameObject.SetActive(false);
        }
    }
}
