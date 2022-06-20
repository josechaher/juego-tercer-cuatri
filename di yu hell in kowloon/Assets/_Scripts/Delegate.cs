using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delegate : MonoBehaviour
{
    public delegate void Contact();
    public event Contact OnContact;

    private void OnColliderEnter(Collider other)
    {
        if (OnContact != null)
            OnContact();
    }
}
