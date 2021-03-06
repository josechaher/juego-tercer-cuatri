using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Todos
public class Player : Entity, ICanInstakill
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

    private bool godMode = false;


    protected override void ArtificialAwake()
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

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            animator.SetTrigger("Punch");
        }

        // Reloads level with R
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            godMode = !godMode;
        }
    }

    public override void TakeDamage(float damage, bool crit = false)
    {
        if (!godMode)
        {
            CurrentHealth -= damage;

            //Life Bar
            slider.value = CurrentHealth / MaxHealth;
        }

        if (CurrentHealth <= 0)
        {
            ChangeScene.currentLevelSceneIndex = SceneManager.GetActiveScene().buildIndex;

            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene("Try Again");
        }
    }

    public void InstakillMethod()
    {
        CurrentHealth = 0;
        if (CurrentHealth <= 0)
        {
            ChangeScene.currentLevelSceneIndex = SceneManager.GetActiveScene().buildIndex;

            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene("Try Again");
        }
    }
}



