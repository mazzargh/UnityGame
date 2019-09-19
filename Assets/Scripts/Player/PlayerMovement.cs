using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody playerRB;
    public float maxSpeed = 5f;
    //public float acceleration;

    private bool facingRight = true;
    public LayerMask groundLayer;
    public float jumpForce = 400;

    public float gravity;
    public float stickToGroundForce;
    public float ySpeed;

    private bool justFell;

    private bool grounded = false;
    private bool jumping = true;

    CharacterController controlTest;


    // Start is called before the first frame update
    void Start()
    {
        //playerRB = GetComponent<Rigidbody>();
        
        controlTest = GetComponent<CharacterController>();
    }

    private void Update()
    {
        grounded = controlTest.isGrounded; //IsGrounded();
        if (grounded && Input.GetKeyDown("w"))
        {   
            Jump();
        }
        //Debug.Log(grounded);
        CalculateGravity();
        float inputDirection = Input.GetAxis("Horizontal");
        Rotate(inputDirection);
        Move();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        

        
        
    }

    private void Move()
    {
        //playerRB.velocity = new Vector3(inputDirection * maxSpeed, playerRB.velocity.y, 0);

        Vector3 velocity = new Vector3(Input.GetAxis("Horizontal") * maxSpeed * Time.deltaTime, ySpeed + stickToGroundForce, 0);
        //transform.Translate(velocity);

        controlTest.Move(velocity);
    }

    private void CalculateGravity()
    {
        if (grounded && ySpeed <= 0)
        {
            ySpeed = -gravity/100;
            stickToGroundForce = -10;

        }
        else
        {
            stickToGroundForce = 0;
            ySpeed -= gravity*Time.deltaTime;
        }
    }

    

    private void Rotate(float inputDirection)
    {
        if ((inputDirection < 0 && facingRight) || (inputDirection > 0 && !facingRight))
        {
            facingRight = !facingRight;
            transform.Rotate(Vector3.up, 180);
        }
        
    }

    /*
    private bool IsGrounded()
    {
        // Fire raycast to check for ground - just fire from centre for now
        float length = 1.1f; // length of raycast to search for ground
        RaycastHit hit;
        Debug.DrawRay(transform.position, -Vector3.up, Color.red);
        if (Physics.Raycast(transform.position, -Vector3.up, out hit, length, groundLayer))
        {
            return true;
        }
        return false;
    }
    */

    private void Jump()
    {
        ySpeed = jumpForce;
        //Debug.Log(ySpeed);
    }
}
