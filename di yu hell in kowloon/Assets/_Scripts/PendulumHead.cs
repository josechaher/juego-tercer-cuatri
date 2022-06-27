using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PendulumHead : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        ICanInstakill instakillinterface = other.GetComponent<ICanInstakill>();
        if (instakillinterface != null)
        {
            instakillinterface.InstakillMethod();
        }
    }
}
