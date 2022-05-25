using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterController))]

public class Player : Entity
{

    // IDLE
    // GROUNDED
    // MOVING

    // JUMPING
    // FALLING

    // CLIMBING
    Player_Movement _move;

    [SerializeField] private Slider slider;

    public Text debug;

    //Movimiento
    Vector3 moveDirection;
    public float moveSpeed = 5;
    public CharacterController cr;

    //turn
    public Vector2 mousePosition;
    public float lookSensitivity = 2;


    //Salto
    [SerializeField] float jumpHeight = 5;
    public Vector3 velocity;

    [SerializeField] float gravity = -10;

    float distToGround;



    //Mano
    [SerializeField] Material handMaterial;
    [SerializeField] Renderer hand;
    [SerializeField] ParticleSystem handParticles;

    //Grounded
    [SerializeField] bool isGrounded;

    //emmissive esfera
    public bool glowing = false;
    public float glowIntensity = 0;
    Color color;

    //esfera
    [SerializeField] Transform ballSpawn;
    [SerializeField] GameObject ballPrefab;
    private GameObject ball;

    //Animator
    [SerializeField] private Animator animator;

    private static float health = 100;

    private void Awake()
    {
        cr = GetComponent<CharacterController>();
        SetHealth(health);
    }


    void Start()
    {
        handMaterial = hand.GetComponent<Renderer>().material;
        color = handMaterial.GetColor("_EmissionColor");
        Cursor.lockState = CursorLockMode.Locked;
        distToGround = cr.height / 2;
        SetHealth(health);
        _move = new Player_Movement(moveDirection, moveSpeed, cr, transform, mousePosition, lookSensitivity, jumpHeight, velocity, gravity, distToGround, isGrounded, debug);
    }

    // ArtificialUpdate is called on Entity's update function
    protected override void ArtificialUpdate()
    {
        #region function calls

        _move.MovementUpdate();

        #endregion
        #region SHOOTING
        if (Input.GetMouseButtonDown(0))
        {

            ball = Instantiate(ballPrefab, Camera.main.transform);
            ball.transform.position = ballSpawn.position;

            handParticles.Play();

            glowIntensity = 0;
            handMaterial.EnableKeyword("_EMISSION");
            glowing = true;
            animator.SetTrigger("Charge");
        }
        if (Input.GetMouseButtonUp(0))
        {
            handParticles.Stop();
            ball.GetComponent<Ball>().Shoot();
            handMaterial.DisableKeyword("_EMISSION");
            glowing = false;
            animator.SetTrigger("Shoot");
        }
        if (glowing)
        {
            glowIntensity += 0.5f * Time.deltaTime;
            handMaterial.SetColor("_EmissionColor", color * glowIntensity);
        }
        #endregion

        //Life Bar
        slider.value = CurrentHealth / MaxHealth;

        if (CurrentHealth <= 0)
        {
            Destroy(gameObject);            
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Destroy(gameObject);
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Bullet")
        {
            CurrentHealth -= 25;
            Destroy(hit.gameObject);
        }
    }

    public override void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
    }

    private void OnDestroy()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}



