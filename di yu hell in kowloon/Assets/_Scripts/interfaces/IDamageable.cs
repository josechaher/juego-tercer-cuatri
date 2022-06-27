using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable<T>
{
    void TakeDamage(float damage);
}

public interface ICanInstakill
{
    public void InstakillMethod();
}