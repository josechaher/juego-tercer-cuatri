using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : Enemy
{
    private float health = 100;

    private void Awake()
    {
        SetHealth(health);
    }


    protected override void ArtificialUpdate()
    {
        //Make minion patrol

        //Make minion chase

        //Make minion attack
    }
}
