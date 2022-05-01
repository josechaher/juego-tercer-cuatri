using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] SlidingDoor slidingDoor;

    private void OnCollisionEnter(Collision collision)
    {
        Ball ball = collision.gameObject.GetComponent<Ball>();

        if (ball)
        {
            slidingDoor.TargetDestroyed(this);
        }
    }
}
