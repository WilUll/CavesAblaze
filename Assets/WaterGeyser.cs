using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGeyser : MonoBehaviour
{
    Rigidbody2D waterSprite;
    public int startPos, endPos;
    float startSpeed, backSpeed;
    bool isPlayerClose = true;
    bool isShooting = true;
    // Start is called before the first frame update
    void Start()
    {
        waterSprite = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerClose)
        {
            if (isShooting)
            {
                if (waterSprite.transform.position.y <= endPos)
                {
                    waterSprite.velocity += Vector2.up * 0.01f;
                }
                else
                {
                    StartCoroutine(varTimer());
                    IEnumerator varTimer()
                    {
                        waterSprite.velocity = Vector2.zero;
                        yield return new WaitForSeconds(2);
                        isShooting = false;
                    }
                }
            }
            else
            {
                if (waterSprite.transform.position.y >= startPos)
                {
                    waterSprite.velocity += Vector2.down * 0.05f;
                }
                else
                {
                    StartCoroutine(varTimer());
                    IEnumerator varTimer()
                    {
                    waterSprite.velocity = Vector2.zero;
                    yield return new WaitForSeconds(5);
                    isShooting = true;
                    }
                }

            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isPlayerClose = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isPlayerClose = false;
    }


}
