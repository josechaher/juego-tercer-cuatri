using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashAttack : MonoBehaviour
{
    [SerializeField] private float attackTime;
    private float attackSpeed;
    [SerializeField] Color startColor = new Color(1, 1, 1, 0.1f);
    [SerializeField] Color finalColor = new Color(1, 0, 0, 0.9f);

    [SerializeField] ParticleSystem chargeParticles;
    [SerializeField] ParticleSystem explodeParticles;

    MeshRenderer meshRenderer;

    private BigDemon bigDemon;

    // Start is called before the first frame update
    void Start()
    {
        bigDemon = transform.parent.GetComponent<BigDemon>();

        meshRenderer = GetComponent<MeshRenderer>();

        attackSpeed = 1 / attackTime;
        StartCoroutine(changeColors(attackSpeed));
    }

    IEnumerator changeColors(float speed)
    {
        ParticleSystem chargePS = Instantiate(chargeParticles, bigDemon.chest.position, Quaternion.identity, bigDemon.chest);

        var t = 0.0f;
        while (t <= 1.0f)
        {
            t += speed * Time.deltaTime;
            meshRenderer.material.color = Color.Lerp(startColor, finalColor, t);
            yield return null;
        }

        chargePS.Stop();
        Collider collider = GetComponent<Collider>();        

        ParticleSystem expPS = Instantiate(explodeParticles, transform.position, Quaternion.identity);
        expPS.transform.eulerAngles = new Vector3(-90, 0, 0);
        bigDemon.SplashDamage();
        Destroy(gameObject);
    }


}
