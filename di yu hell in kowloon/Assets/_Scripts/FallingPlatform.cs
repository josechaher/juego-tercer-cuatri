using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    bool isFalling = false;
    bool playerEntered = false;
    float offsetFallTime = 2;
    float downSpeed = 0;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "Player")
            playerEntered = true;

    }

    void Update()
    {
        if (playerEntered)
        {
            offsetFallTime -= Time.deltaTime;
            if (offsetFallTime <=0)
            {
                isFalling = true;
            }
        }

        if (isFalling)
        {
            downSpeed += Time.deltaTime/10;
            transform.position = new Vector3(transform.position.x, transform.position.y - downSpeed, transform.position.z);
        }
    }
}
