using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    //Audio Clips
    public AudioClip[] footstep_sounds;
    public AudioClip jump_sound;
    public AudioClip land_sound;

    public static GameAssets Instance;

    private void Awake()
    {
        Instance = this;
    }

}
