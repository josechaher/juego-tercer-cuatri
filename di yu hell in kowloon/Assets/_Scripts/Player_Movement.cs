using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Player_Movement 
{
    public Text debug;

    //walk
    Vector3 _moveDirection;
    float _moveSpeed ;
    CharacterController _cr;
    Transform _t;
    
    //turn
    Vector2 _mousePosition;
    float _lookSensitivity ;

    float timeBetweenFootsteps = 0.5f;
    float timeUntilNextFoostep;
    int numberOfFootstepSounds;

    

    //salto
    float _jh ; //jumpHeight
    Vector3 _v;              //velocity
    float _g ;//gravity
    float _dtg; //distToGround

    bool _isGrounded;
    // Start is called before the first frame update

    public Player_Movement(Vector3 moveDirection, float moveSpeed, CharacterController cr, Transform t, Vector2 mousePosition, float lookSensitivity,
                            float jumpHeight, Vector3 velocity, float gravity, float distToGround, bool isGrounded, Text debug)
    {
        _moveDirection = moveDirection;
        _moveSpeed = moveSpeed;
        _cr = cr;
        _t = t;
        _mousePosition = mousePosition;
        _lookSensitivity = lookSensitivity;
        _jh = jumpHeight;
        _v = velocity;
        _g = gravity;
        _dtg = distToGround;
        this._isGrounded = isGrounded;
        this.debug = debug;

        timeUntilNextFoostep = timeBetweenFootsteps;
        numberOfFootstepSounds = GameAssets.Instance.footstep_sounds.Length;
    }

    public void MovementUpdate()
    {
        //GRAVITY
        GravityUpdate();

        // PLAYER ROTATION
        Turn();

        // PLAYER WALK
        Walk();

        //GROUND CHECK
        GroundCheck();

        // JUMP
        Jump();
    }



    public void Walk()
    {
        _moveDirection = _t.right * Input.GetAxisRaw("Horizontal")
                    + _t.forward * Input.GetAxisRaw("Vertical");

        // If player is moving
        if (_moveDirection != Vector3.zero)
        {
            Debug.Log("moviendose");
            _cr.Move(_moveDirection.normalized * _moveSpeed * Time.deltaTime);
            _moveDirection = Vector3.zero;

            // Footstep sounds
            if (_isGrounded)
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
        _mousePosition.x += Input.GetAxisRaw("Mouse X") * _lookSensitivity;
        _mousePosition.x %= 360; // Snaps rotation angle back to 0 when it reaches 360 or -360 degrees

        _mousePosition.y -= Input.GetAxisRaw("Mouse Y") * _lookSensitivity;
        _mousePosition.y = Mathf.Clamp(_mousePosition.y, -90, 90); // Prevents camera from rotating past its vertical axis

        _t.rotation = Quaternion.Euler(Vector3.up * _mousePosition.x);
        Camera.main.transform.localRotation = Quaternion.Euler(Vector3.right * _mousePosition.y);
    }

    public void GravityUpdate()
    {
        _v.y += _g * Time.deltaTime;

        if (!_isGrounded)
        {
            _cr.Move(_v * Time.deltaTime);
        }
    }

    public void Jump()
    {
        
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            Debug.Log("estoy en el jump");
            // Velocity is set so that player will reach desired jump height and then start falling
            _v.y = Mathf.Sqrt(-2 * _g * _jh);
            _isGrounded = !_isGrounded;

            AudioManager.Instance.Play(GameAssets.Instance.jump_sound);
        }
    }

    public void GroundCheck()
    {
        RaycastHit hit;

        Debug.Log("estoy en el grundcheck");

        // Checks Player's distance from the ground
        if (Physics.SphereCast(_t.position, 0.5f, Vector3.down, out hit, _dtg - 0.41f))
        {
            Debug.Log("estoy en el raycast");
            // Ignore collisions in "Player" layer

            if (!hit.collider.isTrigger)
            {
                if (!_isGrounded)
                {
                    AudioManager.Instance.Play(GameAssets.Instance.land_sound);
                    _isGrounded = true;
                    debug.text = "Grounded";
                }
                _v.y = 0;
            }          

        }
        else
        {
            _isGrounded = false;
            debug.text = "Not Grounded";
        }
    }

}
