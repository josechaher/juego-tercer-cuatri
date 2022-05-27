using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    private AudioSource audioSource;

    private void Awake()
    {
        Instance = this;
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void Play(AudioClip clip) 
    {
        audioSource.PlayOneShot(clip);
    }
}
