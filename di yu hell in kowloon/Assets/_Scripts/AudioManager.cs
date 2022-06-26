using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    private AudioSource audioSource;
    public StructSound[] sound;

    [SerializeField] Slider volumeSlider;

    [SerializeField][Range(0, 1)]
    private float musicVolume = 0.5f;

    public Dictionary<string, StructSound> _sounds = new Dictionary<string, StructSound>();

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();

        if (!PlayerPrefs.HasKey("MasterVolume"))
        {
            PlayerPrefs.SetFloat("MasterVolume", 0.2f);
        }

        volumeSlider.value = PlayerPrefs.GetFloat("MasterVolume");
        AudioListener.volume = volumeSlider.value;

        Instance = this;

        foreach (var item in sound)
        {
            if (!_sounds.ContainsKey(item.name))
                _sounds.Add(item.name, item);
        }
    }

    public void Play(string name, int n = 0)
    {
        if (_sounds.ContainsKey(name))
        {
            if (_sounds[name].type == TypesSound.music)
                PlayMusic(name, n);
            else
                audioSource.PlayOneShot(_sounds[name].clip[n]);

        }
    }

    public void Play(string name, Transform transform, int n = 0)
    {
        if (_sounds.ContainsKey(name))
        {
            AudioSource audioSource = transform.gameObject.AddComponent<AudioSource>();

            StructSound sound = _sounds[name];
            audioSource.clip = sound.clip[n];
            audioSource.loop = sound.loop;
            audioSource.spatialBlend = 1;
            audioSource.Play();
        }
    } 

    public enum TypesSound
    {
        background,
        player_sfx,
        object_sfx,
        music,
    }

    private void PlayMusic(string name, int n)
    {
        AudioSource musicSource = gameObject.AddComponent<AudioSource>();

        StructSound sound = _sounds[name];
        musicSource.clip = sound.clip[n];
        musicSource.loop = sound.loop;
        musicSource.volume = musicVolume;

        musicSource.Play();
    }

    public void changeVolume()
    {
        PlayerPrefs.SetFloat("MasterVolume", volumeSlider.value);
        AudioListener.volume = volumeSlider.value;
    }
}
