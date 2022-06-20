using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Delegate doorSwitch;

    void Start()
    {
        doorSwitch.OnContact += Open;
    }

    private void Open()
    {
        transform.Translate(Vector3.up * 3f);
    }

    private void OnDisable()
    {
        doorSwitch.OnContact -= Open;
    }
}
