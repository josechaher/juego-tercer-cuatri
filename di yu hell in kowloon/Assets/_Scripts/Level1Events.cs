using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Diego Salazar, Jose Chaher
public class Level1Events : MonoBehaviour
{
    [SerializeField] string music_clip;


    public void Start()
    {
        AudioManager.Instance.Play(music_clip);
    }
}
