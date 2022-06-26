using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Level3Events : MonoBehaviour
{
    [SerializeField] string music_clip;

    public void Start()
    {
        AudioManager.Instance.Play(music_clip);
    }
    public void BeholderDestroyed() {
        StartCoroutine(ChangeScene.DelaySceneChange("Congrats", 3));
    }
}
