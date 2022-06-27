using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Diego Salazar
[System.Serializable]
public struct StructSound
{
    public string name;
    public AudioClip[] clip;
    public bool loop;
    public AudioManager.TypesSound type;
}
