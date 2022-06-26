using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    // DAMAGE, BOUNCES, SPEED,
    // SCALE/SIZE, EMISSION, TRANSPARENCY, SHAKE MAGNITUDE - SMOOTHLERP

    private bool shooting = false;
    private bool charging = true;

    private float charge = 0f;
    private float maxCharge = 100f;

    private float size = 0f;
    private float maxSize = 0.5f;

    public float chargeSpeed = 40f;
    
    private float speed = 0f;
    private float maxSpeed = 40f;

    private float maxDamage = 50f;

    private int bounces = 0;
    private int maxBounces = 4;

    private float yVelocity = 5;


//    private float chargeSpeed = 1;

    private float emissionIntensity = 0f;
    private float maxIntensity = 2f;
    private Color emissionColor;

    Material material;
    Color ogColor;

    Vector3 startPosition;

    [SerializeField] TrailRenderer tr;

    [SerializeField] Rigidbody rb;

    public float shakeMagnitude = 0;
    public float maxShake = 0.02f;
    public float shakeSpeed = 10;

    // Start is called before the first frame update
    void Start()
    {
        
        transform.localScale = Vector3.zero;

        material = GetComponent<Renderer>().material;
        emissionColor = material.GetColor("_EmissionColor");

        ogColor = material.color;
        ogColor.a = 0;

        material.EnableKeyword("_EMISSION");

        startPosition = transform.localPosition;

        StartCoroutine(Shake());
    }

    // Update is called once per frame
    void Update()
    {
        if (charging)
        {
            charge += chargeSpeed * Time.deltaTime;

            // SIZE
            size = (charge * maxSize) / maxCharge;
            transform.localScale = Vector3.one * size;

            // EMISSION
            emissionIntensity = (charge * maxIntensity) / maxCharge;
            material.SetColor("_EmissionColor", emissionColor * emissionIntensity);

            // ALPHA
            ogColor.a = charge / maxCharge;
            material.color = ogColor;

            // SHAKE MAGNITUDE
            shakeMagnitude = (charge * maxShake) / maxCharge;

            if (charge > maxCharge)
            {
                charge = maxCharge;

                transform.localScale = Vector3.one * maxSize;

                material.SetColor("_EmissionColor", emissionColor * maxIntensity);

                ogColor.a = 1;
                material.color = ogColor;

                shakeMagnitude = maxShake;

                charging = false;
            }
        }
    }

    public void Shoot()
    {
        charging = false;
        transform.localPosition = startPosition;
        transform.parent = null;
        Destroy(gameObject, 20);
        shooting = true;

        transform.SetLayerRecursively("Player");

        speed = (charge * maxSpeed) / maxCharge;

        bounces = ((int) charge * maxBounces) / (int) maxCharge;

        tr.gameObject.SetActive(true);
        tr.widthMultiplier = transform.localScale.x;
        rb.isKinematic = false;
        rb.AddForce(transform.forward * speed + Vector3.up * ((charge * yVelocity) / maxCharge), ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        bounces -= 1;
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy)
        {
            float baseDamage = (charge * maxDamage) / maxCharge;
            bool crit = enemy.critCollider == collision.collider;
            enemy.TakeDamage(baseDamage * (crit ? 2 : 1));
            Instantiate(enemy.bloodParticles, transform.position, Quaternion.identity, enemy.transform);
        }

        if (bounces <= 0)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator Shake()
    {
        while(!shooting)
        {
            transform.localPosition = startPosition + Random.insideUnitSphere * shakeMagnitude;
            yield return new WaitForSeconds(1f/shakeSpeed);
        }
    }
}
