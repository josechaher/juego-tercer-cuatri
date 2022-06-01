using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SealsManager : MonoBehaviour
{
    public static SealsManager Instance;

    private int points = 0;
    private int sealCount;

    [SerializeField] AudioClip seal_acquired;

    private void Awake()
    {
        Instance = this;
        sealCount = FindObjectsOfType<Seal>().Length;
    }

    public void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 20), "Seals : " + points);
    }

    public void SealCollected(Seal seal)
    {
        print("SEAL COLLECTED function called");
        Destroy(seal.gameObject);

        points++;

        AudioManager.Instance.Play(seal_acquired);

        if (points >= sealCount)
        {
            Instance = null;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        }
    }
}
