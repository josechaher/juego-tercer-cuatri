using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Pedro Chiswell
public interface IDamageable<T>
{
    void TakeDamage(float damage);
}

public interface ICanInstakill
{
    public void InstakillMethod();
}