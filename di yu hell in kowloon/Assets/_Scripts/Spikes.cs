using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        ICanInstakill spikeinterface = other.GetComponent<ICanInstakill>();
        if (spikeinterface != null)
        {
            spikeinterface.InstakillMethod();
        }
    }
}
