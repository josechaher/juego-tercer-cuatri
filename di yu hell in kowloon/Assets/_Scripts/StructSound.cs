using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct StructSound
{
    public string name;
    public AudioClip[] audio;
    public bool loop;
    public AudioManager.TypesSound type;
  

}
