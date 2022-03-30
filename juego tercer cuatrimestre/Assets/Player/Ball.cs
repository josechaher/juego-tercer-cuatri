using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private bool shooting = false;
    private bool charging = true;
    private float speed = 20;
    private float acceleration = -5;

    private float gravity = -10;
    private float yVelocity = 5;

    private float chargeSpeed = 1;

    private float emissionIntensity = 0;
    private Color emissionColor;

    Material glow;

    [SerializeField] TrailRenderer tr;

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = Vector3.zero;

        glow = GetComponent<Renderer>().material;
        emissionColor = glow.GetColor("_EmissionColor");

        glow.EnableKeyword("_EMISSION");
    }

    // Update is called once per frame
    void Update()
    {
        if (charging)
        {
            emissionIntensity += chargeSpeed * 2 * Time.deltaTime;
            glow.SetColor("_EmissionColor", emissionColor * emissionIntensity);


            transform.localScale += Vector3.one * chargeSpeed * Time.deltaTime;

            if (transform.localScale.x > 0.5f)
            {
                charging = false;
            }
        }

        if (shooting)
        {
            transform.position += transform.forward * speed * Time.deltaTime;

            transform.position += Vector3.up * yVelocity * Time.deltaTime;

            yVelocity += gravity * Time.deltaTime;

            speed += acceleration * Time.deltaTime;
            
        }
    }

    public void Shoot()
    {
        charging = false;
        transform.parent = null;
        Destroy(gameObject, 4);
        shooting = true;

        tr.gameObject.SetActive(true);
        tr.widthMultiplier = transform.localScale.x;
    }
}
