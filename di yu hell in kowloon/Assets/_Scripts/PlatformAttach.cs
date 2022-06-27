using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Rodrigo Chiale, Jose Chaher
public class PlatformAttach : MonoBehaviour
{
    public GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        print("platform collision");
        if (other.gameObject == player)
        {
            print("player is on top");
            player.transform.parent = transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            player.transform.parent = null;
        }
    }
}
