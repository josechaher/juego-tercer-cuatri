using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashAttack : MonoBehaviour
{
    [SerializeField] private float attackTime;
    private float attackSpeed;
    [SerializeField] Color startColor = new Color(1, 1, 1, 0.1f);
    [SerializeField] Color finalColor = new Color(1, 0, 0, 0.9f);

    [SerializeField] ParticleSystem explodeParticles;

    Renderer renderer;

    float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<MeshRenderer>();

        attackSpeed = 1 / attackTime;
        StartCoroutine(changeColors(attackSpeed));
    }

    IEnumerator changeColors(float speed)
    {
        var t = 0.0f;
        while (t <= 1.0f)
        {
            t += speed * Time.deltaTime;
            renderer.material.color = Color.Lerp(startColor, finalColor, t);
            yield return null;
        }

        ParticleSystem ps = Instantiate(explodeParticles, transform.position + Vector3.up, Quaternion.identity);
        Destroy(gameObject);
    }
}
