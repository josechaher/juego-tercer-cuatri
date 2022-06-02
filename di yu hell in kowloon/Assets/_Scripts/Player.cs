using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//[RequireComponent(typeof(CharacterController))]

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
    public float moveSpeed = 5;
    Rigidbody rb;

    //turn
    public float lookSensitivity = 2;


    //Salto
    [SerializeField] float jumpHeight = 5;

    //Mano
    [SerializeField] ParticleSystem handParticles;

    //emmissive esfera
    public bool glowing = false;
    public float glowIntensity = 0;

    //esfera
    [SerializeField] Transform ballSpawn;
    [SerializeField] GameObject ballPrefab;
    private GameObject ball;

    //Animator
    [SerializeField] private Animator animator;

    private static float health = 100;

    [SerializeField] LayerMask whatIsGround;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        SetHealth(health);
    }


    void Start()
    {

        Cursor.lockState = CursorLockMode.Locked;
        SetHealth(health);
        _move = new Player_Movement(moveSpeed, rb, transform, lookSensitivity, jumpHeight, debug, whatIsGround);
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
            glowing = true;
            animator.SetTrigger("Charge");
            
        }
        if (Input.GetMouseButtonUp(0))
        {
            handParticles.Stop();
            ball.GetComponent<Ball>().Shoot();
            glowing = false;
            animator.SetTrigger("Shoot");
        }
        if (glowing)
        {
            glowIntensity += 0.5f * Time.deltaTime;
        }
        #endregion

        // Punching

        if (Input.GetKeyDown(KeyCode.E))
        {
            animator.SetTrigger("Punch");
        }

        //Life Bar
        slider.value = CurrentHealth / MaxHealth;

        // Reloads level with R
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public override void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void Die()
    {
        CurrentHealth = 0;
        if (CurrentHealth <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}



