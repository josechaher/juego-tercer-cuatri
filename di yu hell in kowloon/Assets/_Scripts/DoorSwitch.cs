using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSwitch : MonoBehaviour
{
    public delegate void Contact();
    public event Contact OnContact;

    private void OnTriggerEnter(Collider other)
    {
        if (OnContact != null)
            OnContact();
    }

    private void OnTriggerExit(Collider other)
    {
        OnContact = null;
    }
}
