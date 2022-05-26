using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seal : Collector
{
    public void OnTriggerEnter(Collider collision)
    {
        Player player = collision.GetComponent<Player>();

        if (player)
        {
            SealsManager.Instance.SealCollected(this);
        }
    }


}
