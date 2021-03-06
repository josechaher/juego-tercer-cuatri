using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Rodrigo Chiale, Diego Salazar, Jose Chaher
public class Seal : Collector
{
    private void Start()
    {
        AudioManager.Instance.Play("seal_continuous", transform);
    }

    public void OnTriggerEnter(Collider collision)
    {
        Player player = collision.GetComponent<Player>();

        if (player)
        {
            SealsManager.Instance.SealCollected(this);
        }
    }


}
