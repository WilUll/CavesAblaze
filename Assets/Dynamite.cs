using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dynamite : MonoBehaviour
{
    LineRenderer LR;
    public Transform[] fusePos;
    public bool isIgnited;
    public AudioSource audioSource;
    public AudioClip[] clips;
    public GameObject fuseSparks;

    float startTime = 0;
    GameObject particleExplosion;
    ExplodingWall wallScript;
    ParticleSystem explosion;
    ShakeCamera cameraShake;


    public int index;
    // Start is called before the first frame update
    void Start()
    {
        fuseSparks.SetActive(false);
        LR = GetComponent<LineRenderer>();
        LR.positionCount = fusePos.Length;
        for (int i = 0; i < fusePos.Length; i++)
        {
            LR.SetPosition(i, (fusePos[i].position));
        }
        index = fusePos.Length - 1;
        particleExplosion = gameObject.transform.Find("explotion").gameObject;
        wallScript = GetComponentInParent<ExplodingWall>();
        cameraShake = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ShakeCamera>();
        explosion = particleExplosion.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isIgnited)
        {
            if (Vector2.Distance(fusePos[fusePos.Length - 1].position, fusePos[0].position) > 1f)
            {
                //audioSource.PlayOneShot(clips[1]);
                startTime += Time.deltaTime;
                float leftToGo = (startTime / 200);
                LR.SetPosition(index, Vector2.Lerp(fusePos[index].position, fusePos[index - 1].position, leftToGo));
                if (index == fusePos.Length - 2)
                {
                    LR.SetPosition(index + 1, LR.GetPosition(index));
                    fusePos[index + 1].position = LR.GetPosition(index);

                }
                fusePos[index].position = LR.GetPosition(index);
                if (leftToGo >= 1 || Vector2.Distance(fusePos[index].position, fusePos[index - 1].position) <= 0.2f)
                {
                    startTime = 0;
                    index--;
                }
            }
            else
            {
                StartCoroutine(explodeTimer());
                IEnumerator explodeTimer()
                {
                    isIgnited = false;
                    explosion.Play();
                    wallScript.audioSource.Play();
                    cameraShake.Shake(0.3f, 0.5f);

                    yield return new WaitForSeconds(0.2f);
                    transform.parent.gameObject.GetComponent<ExplodingWall>().DamageWall(transform);
                    gameObject.SetActive(false);
                }

            }
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isIgnited)
        {
            isIgnited = true;
            fuseSparks.SetActive(true);

            StartCoroutine(playFuseLoop()); 
            IEnumerator playFuseLoop()
            {
                audioSource.PlayOneShot(clips[0]);
                yield return new WaitForSeconds(clips[0].length);
                audioSource.PlayOneShot(clips[1]);
                audioSource.loop = true;

            }

            audioSource.PlayOneShot(clips[0]);
        }
    }
}
