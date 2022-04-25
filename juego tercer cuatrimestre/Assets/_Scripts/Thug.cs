using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thug : Enemy
{
    float thugHealth = 75;
    float damage = 25;


    protected override void ArtificialUpdate()
    {
        base.ArtificialUpdate();
    }

    protected override void SetHealth()
    {
        maxHealth = thugHealth;
        health = maxHealth;
    }
}
