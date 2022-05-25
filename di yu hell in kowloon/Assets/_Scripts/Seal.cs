using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seal : Collector
{
    public void OnTriggerEnter(Collider collision)
    {
        if (collision.GetComponent<SealsManager>() != null)
        {
            collision.GetComponent<SealsManager>().points++;
            GetComponent<AudioSource>().Play();
            Destroy(this.gameObject);
        }
    }
}
