using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public DoorSwitch doorSwitch;

    [SerializeField] float speed = 5;
    [SerializeField] float height = 20;

    void Start()
    {
        doorSwitch.OnContact += Open;
    }

    private void Open()
    {
        StartCoroutine(Slide(-Vector3.up));
        doorSwitch.OnContact -= Open;
        doorSwitch.OnContact += Close;
    }

    private void Close()
    {
        StartCoroutine(Slide(Vector3.up));
        doorSwitch.OnContact -= Close;
        doorSwitch.OnContact += Open;
    }

    IEnumerator Slide(Vector3 slideDirection)
    {
        float distanceMoved = 0;
        while (distanceMoved < height)
        {
            transform.position += slideDirection * speed * Time.deltaTime;
            distanceMoved += speed * Time.deltaTime;
            yield return null;
        }
    }
}
