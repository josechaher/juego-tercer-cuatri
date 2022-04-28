using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thug : Enemy
{
    private static float health = 75;

    private void Awake()
    {
        SetHealth(health);
    }

    protected override void ArtificialUpdate()
    {
        base.ArtificialUpdate();

        //Make thug patrol

        //Make thug chase

        //Make thug attack
    }
}
