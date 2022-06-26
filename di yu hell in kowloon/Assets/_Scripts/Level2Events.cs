using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Events : MonoBehaviour
{
    [SerializeField] string music_clip;


    public void Start()
    {
        AudioManager.Instance.Play(music_clip);
    }
}
