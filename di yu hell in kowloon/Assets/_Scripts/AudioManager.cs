using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    private AudioSource audioSource;
    public StructSound[] sound;

    public Dictionary<string, StructSound> _sounds = new Dictionary<string, StructSound>();

    private void Awake()
    {
        Instance = this;
        audioSource = gameObject.AddComponent<AudioSource>();

        foreach (var item in sound)
        {
            if (!_sounds.ContainsKey(item.name))
                _sounds.Add(item.name, item);
        }
    }

    public void Play(string name, int n = 0)
    {
        if (_sounds.ContainsKey(name))
            audioSource.PlayOneShot(_sounds[name].audio[n]);
    }

    public enum TypesSound
    {
        background,
        player_sfx,
        object_sfx,
    }


}
