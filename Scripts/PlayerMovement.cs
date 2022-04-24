using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Player

{
    public float runSpeed = 7;
    public float rotationSpeed = 250;
    public Animator animator;
    private float x, y;
    public Rigidbody rb;
    public float jumpForce = 4f;
    public bool canJump;

    void start()
    {
        canJump = false;
    }

    void FixedUpdate()
    {
        transform.Rotate(0, x * Time.deltaTime * rotationSpeed, 0);
        transform.Translate(0, 0, y * Time.deltaTime * runSpeed);
    }

    void Update()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");
        animator.SetFloat("VelX", x);
        animator.SetFloat("VelY", y);
       
        if (canJump = true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                animator.SetBool("Jump", true);
                rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            }
            animator.SetBool("Floor", true);
        }
        else
        {
            Falling();
        }
    }

    public void Falling()
    {
        animator.SetBool("Floor", false);
        animator.SetBool("Jump", false);
    }
}
