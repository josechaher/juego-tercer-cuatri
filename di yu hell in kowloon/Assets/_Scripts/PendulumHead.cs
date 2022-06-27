using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Pedro Chiswell
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
