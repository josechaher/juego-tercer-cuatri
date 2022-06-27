using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Rodrigo Chiale, Jose Chaher
public class DoorSwitch : MonoBehaviour
{
    public delegate void Contact();
    public event Contact OnContact;

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if (OnContact != null && player)
            OnContact();
    }

    private void OnTriggerExit(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if (OnContact != null && player)
            OnContact();
    }
}
