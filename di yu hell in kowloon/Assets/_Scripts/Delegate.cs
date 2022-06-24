using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delegate : MonoBehaviour
{
    public delegate void Contact();
    public event Contact OnContact;

    private void onCollisionEnter(Collision Player)
    {
        if (OnContact != null)
            OnContact();
    }
}
