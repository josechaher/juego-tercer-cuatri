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

            AudioManager.Instance.Play("target_hit", transform);
            GetComponent<MeshRenderer>().enabled = false; //Removes object from view
            GetComponent<BoxCollider>().enabled = false; //Prevents possible future collisions with invisible object
            Destroy(gameObject, 2); // Delays object destruction to let sound play
        }
    }
}
