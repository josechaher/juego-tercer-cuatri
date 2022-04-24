using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingplatform : MonoBehaviour
{
    public GameObject platform;
    public float movespeed;
    public Transform currentpoint;
    public Transform[] points;
    public int pointselection;

    void Start()
    {
        currentpoint = points[pointselection];
    }

    void Update()
    {
        platform.transform.position = Vector3.MoveTowards(platform.transform.position, currentpoint.position, Time.deltaTime * movespeed);
        if (platform.transform.position == currentpoint.position)
        {
            pointselection++;
            if (pointselection == points.Length)
            {
                pointselection = 0;
            }
            currentpoint = points[pointselection];
        }
    }
}
