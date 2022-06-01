using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    bool isFalling = false;
    bool playerEntered = false;
    float fallTimer;
    float fallTime = 2;
    float downSpeed = 0;
    float resetTime = 5;
    Vector3 initialPosition;

    private void Start()
    {
        initialPosition = this.transform.position;
        fallTimer = fallTime;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "Player")
            playerEntered = true;
    }

    void Update()
    {
        if (playerEntered)
        {
            fallTimer -= Time.deltaTime;
            if (fallTimer <=0)
            {
                StartCoroutine(Reset());
                playerEntered = false;
                isFalling = true;
            }
        }

        if (isFalling)
        {
            downSpeed += Time.deltaTime/10;
            transform.position = new Vector3(transform.position.x, transform.position.y - downSpeed, transform.position.z);
        }
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(resetTime);
        transform.position = initialPosition;
        playerEntered = false;
        isFalling = false;
        fallTimer = fallTime;
    }
}
