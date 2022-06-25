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
        StartCoroutine(Slide());
    }

    IEnumerator Slide()
    {
        float distanceMoved = 0;
        while (distanceMoved < height)
        {
            transform.position += -Vector3.up * speed * Time.deltaTime;
            distanceMoved += speed * Time.deltaTime;
            yield return null;
        }
    }

    private void OnDisable()
    {
        doorSwitch.OnContact -= Open;
    }
}
