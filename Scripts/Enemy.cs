using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Life
{
    // Start is called before the first frame update
    public GameObject platform;
    public float movespeed;
    public Transform currentpoint;
    public Transform[] points;
    public int pointselection;
    public string name;

    void Start()
    {
        currentpoint = points[pointselection];
    }

    // Update is called once per frame
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
    
    public override void takeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
