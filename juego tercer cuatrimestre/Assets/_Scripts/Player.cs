using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterController))]

public class Player : MonoBehaviour
{

    public int points;

    // IDLE
    // GROUNDED
    // MOVING

    // JUMPING
    // FALLING

    // CLIMBING


    public Text debug;

    Vector3 moveDirection;
    [SerializeField] float moveSpeed = 5;

    public Vector2 mousePosition;
    [SerializeField] float lookSensitivity = 2;

    [SerializeField] float jumpHeight = 5;
    public Vector3 velocity;

    [SerializeField] float gravity = -10;

    [SerializeField] float distToGround;

    CharacterController cr;

    [SerializeField] private Material handMaterial;
    [SerializeField] private Renderer hand;

    [SerializeField] ParticleSystem handParticles;

    bool isGrounded;

    public bool glowing = false;
    public float glowIntensity = 0;
    Color color;

    [SerializeField] Transform ballSpawn;
    [SerializeField] GameObject ballPrefab;
    GameObject ball;

    [SerializeField] private Animator animator;

    private void Awake()
    {
        cr = GetComponent<CharacterController>();
        distToGround = cr.height / 2;
    }

    
    void Start()
    {
        handMaterial = hand.GetComponent<Renderer>().material;
        color = handMaterial.GetColor("_EmissionColor");
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        #region MOVEMENT
        //GRAVITY
        velocity.y += gravity * Time.deltaTime;

        cr.Move(velocity * Time.deltaTime);        

        // PLAYER ROTATION
        Turn();

        // PLAYER WALK
        Walk();

        // RUN
        if (Input.GetKeyDown(KeyCode.LeftShift)) moveSpeed *= 2;        
        if (Input.GetKeyUp(KeyCode.LeftShift)) moveSpeed /= 2;

        //GROUND CHECK
        GroundCheck();

        // JUMP
        Jump();
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

    }

    /// <summary>
    /// Walk towards where player is facing
    /// </summary>
    void Walk()
    {
        moveDirection = transform.right * Input.GetAxisRaw("Horizontal")
                    + transform.forward * Input.GetAxisRaw("Vertical");

        cr.Move(moveDirection.normalized * moveSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Rotates player and camera according to mouse movements
    /// </summary>
    void Turn()
    {
        mousePosition.x += Input.GetAxisRaw("Mouse X") * lookSensitivity; 
        mousePosition.x %= 360; // Snaps rotation angle back to 0 when it reaches 360 or -360 degrees

        mousePosition.y -= Input.GetAxisRaw("Mouse Y") * lookSensitivity; 
        mousePosition.y = Mathf.Clamp(mousePosition.y, -90, 90); // Prevents camera from rotating past its vertical axis

        transform.rotation = Quaternion.Euler(Vector3.up * mousePosition.x);
        Camera.main.transform.localRotation = Quaternion.Euler(Vector3.right * mousePosition.y);
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            // Velocity is set so that player will reach desired jump height and then start falling
            velocity.y = Mathf.Sqrt(-2 * gravity * jumpHeight);
        }
    }


    void GroundCheck()
    {
        RaycastHit hit;        

        // Checks Player's distance from the ground
        if (Physics.SphereCast(transform.position, 0.5f, Vector3.down, out hit, distToGround - 0.41f))
        {
            // Ignore collisions in "Player" layer
            if (!(hit.collider.gameObject.layer == 3))
            {
                isGrounded = true;
                debug.text = "Grounded";
                velocity.y = 0;
            }
            

            
        } else
        {
            isGrounded = false;
            debug.text = "Not Grounded";
        }
    }

    public void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 20), "Seals : " + points);
        if (points >= 2)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
