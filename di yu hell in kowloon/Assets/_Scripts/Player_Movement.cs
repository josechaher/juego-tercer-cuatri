using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Player_Movement 
{
    public Text _debug;

    //walk
    Vector3 moveDirection;
    float _moveSpeed ;
    Rigidbody _rb;
    Transform _transform;
    
    //turn
    Vector2 mousePosition;
    float _lookSensitivity ;

    float timeBetweenFootsteps = 0.5f;
    float timeUntilNextFoostep;
    int numberOfFootstepSounds;


    //salto
    float _jumpHeight ; //jumpHeight
    float jumpForce;

    bool isGrounded;
    // Start is called before the first frame update

    LayerMask _whatIsGround;

    public Player_Movement(float moveSpeed, Rigidbody rb, Transform transform, float lookSensitivity,
                            float jumpHeight, Text debug, LayerMask whatIsGround)
    {
        _moveSpeed = moveSpeed;
        _rb = rb;
        _transform = transform;
        _lookSensitivity = lookSensitivity;
        _jumpHeight = jumpHeight;
        _debug = debug;
        _whatIsGround = whatIsGround;

        timeUntilNextFoostep = timeBetweenFootsteps;
        numberOfFootstepSounds = GameAssets.Instance.footstep_sounds.Length;

        // Velocity is set so that player will reach desired jump height and then start falling
        jumpForce = Mathf.Sqrt(-2 * Physics.gravity.y * _jumpHeight);
    }

    public void MovementUpdate()
    {
        // PLAYER ROTATION
        if (Time.timeScale != 0)
        {
            Turn();
        }

        // PLAYER WALK
        Walk();

        //GROUND CHECK
        GroundCheck();

        // JUMP
        Jump();
    }



    public void Walk()
    {
        moveDirection = _transform.right * Input.GetAxisRaw("Horizontal")
                    + _transform.forward * Input.GetAxisRaw("Vertical");

        //_rb.velocity = _moveDirection.normalized * _moveSpeed;
        _rb.velocity = new Vector3(moveDirection.normalized.x * _moveSpeed, _rb.velocity.y, moveDirection.normalized.z * _moveSpeed);

        // If player is moving
        if (moveDirection != Vector3.zero)
        {
            // Footstep sounds
            if (isGrounded)
            {
                if (timeUntilNextFoostep < 0)
                {
                    AudioManager.Instance.Play(GameAssets.Instance.footstep_sounds[Random.Range(0, numberOfFootstepSounds)]);
                    timeUntilNextFoostep = timeBetweenFootsteps;
                }
                timeUntilNextFoostep -= Time.deltaTime;
            }
        }

        // RUN
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            timeBetweenFootsteps /= 2;
            timeUntilNextFoostep /= 2;
            _moveSpeed *= 2;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            timeBetweenFootsteps *= 2;
            timeUntilNextFoostep *= 2;
            _moveSpeed /= 2;
        }
    }

    /// <summary>
    /// Rotates player and camera according to mouse movements
    /// </summary>
    public void Turn()
    {
        mousePosition.x += Input.GetAxisRaw("Mouse X") * _lookSensitivity;
        mousePosition.x %= 360; // Snaps rotation angle back to 0 when it reaches 360 or -360 degrees

        mousePosition.y -= Input.GetAxisRaw("Mouse Y") * _lookSensitivity;
        mousePosition.y = Mathf.Clamp(mousePosition.y, -90, 90); // Prevents camera from rotating past its vertical axis

        _transform.rotation = Quaternion.Euler(Vector3.up * mousePosition.x);
        Camera.main.transform.localRotation = Quaternion.Euler(Vector3.right * mousePosition.y);
    }

    public void Jump()
    {
        
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            _rb.AddForce(_transform.up * jumpForce, ForceMode.Impulse);

            AudioManager.Instance.Play(GameAssets.Instance.jump_sound);
        }
    }

    public void GroundCheck()
    {
        RaycastHit hit;

        // Checks Player's distance from the ground
        if (Physics.Raycast(_transform.position - Vector3.down * 0.5f, -_transform.up, out hit, 0.6f, _whatIsGround))
        {
            if (!isGrounded)
            {
                AudioManager.Instance.Play(GameAssets.Instance.land_sound);
                isGrounded = true;
                _debug.text = "Grounded";
            }
        }
        else
        {
            if (isGrounded)
            {
                isGrounded = false;
                _debug.text = "Not Grounded";
            }
        }
    }

}
