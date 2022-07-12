using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashAttack : MonoBehaviour
{
    [SerializeField] private float attackSpeed;
    [SerializeField] Color startColor = new Color(1, 1, 1, 0.1f);
    [SerializeField] Color finalColor = new Color(1, 0, 0, 0.9f);

    Color currentColor;

    Renderer renderer;

    float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<MeshRenderer>();
        currentColor = startColor;

        StartCoroutine(changeColors(attackSpeed));
    }

    IEnumerator changeColors(float speed)
    {
        var t = 0.0f;
        while (t <= 1.0f)
        {
            t += speed * Time.deltaTime;
            currentColor = Color.Lerp(currentColor, finalColor, t);
            renderer.material.color = currentColor;
            yield return null;
        }
    }
}
