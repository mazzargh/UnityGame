using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody playerRB;
    public float maxSpeed = 5f;
    public Collider playerCollider;
    //public float acceleration;

    private bool facingRight = true;
   // public LayerMask groundLayer;
    public float jumpForce = 1;

    public float gravity;
    public float stickToGroundForce;
    public float ySpeed;
    private float hzSpeed;
    public float wallSlideSpeed;
    public float wallJumpForce = 1;

    private bool grounded = false;
    private bool jumping = true;
    private bool wallSliding = false;

    private bool centreMassGrounded = false;
    private bool wallToRight = false;
    private bool wallToLeft = false;
    private bool hitCeiling = false;

    CharacterController controlTest;

    private float inputDirection;
    public Vector3 impactVector = Vector3.zero;

    public Collider passPlatform;


    // Start is called before the first frame update
    void Start()
    {
        //playerRB = GetComponent<Rigidbody>();

        controlTest = GetComponent<CharacterController>();
    }

    private void Update()
    {
        inputDirection = Input.GetAxis("Horizontal");
        grounded = controlTest.isGrounded;
        wallToLeft = CheckPlayerCollision(-Vector3.right, 1f, 1 << 10);
        wallToRight = CheckPlayerCollision(Vector3.right, 1f, 1 << 10);
        hitCeiling = CheckPlayerCollision(Vector3.up, 1.15f, 1 << 9 | 1 << 10);

        if ((controlTest.collisionFlags & CollisionFlags.Above) != 0 && (ySpeed > 0 || impactVector.y > 0))
        {
            Debug.Log("hitting");
            ySpeed = -gravity/100;
            impactVector.y = 0;
        }
        
        if (!grounded && IsFacingWall() && inputDirection != 0 && ySpeed <= 0)
        {
            // wall slide
            wallSliding = true;
            ySpeed = -wallSlideSpeed*Time.deltaTime;
        }
        else if (wallSliding)
        {
            wallSliding = false;
        }

        if (Input.GetKeyDown("w"))
        {   
            if (grounded)
            {
                Jump();
            }
            else if (NextToWall())
            {
                WallJump();
            }
        }

        if (Input.GetKeyDown("s"))
        {
            Physics.IgnoreCollision(playerCollider, passPlatform, true);
        }
        
        CalculateGravity();
        Rotate();
        Move();

        impactVector = Vector3.Lerp(impactVector, Vector3.zero, 5 * Time.deltaTime);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
   
    }

    private void Move()
    {
        //playerRB.velocity = new Vector3(inputDirection * maxSpeed, playerRB.velocity.y, 0);

        Vector3 velocity = new Vector3(inputDirection * maxSpeed * Time.deltaTime, ySpeed + stickToGroundForce, 0);
        //transform.Translate(velocity);

        controlTest.Move(velocity + impactVector);
    }

    private void CalculateGravity()
    {
        if (grounded && ySpeed <= 0)
        {
            ySpeed = -gravity/100;
            centreMassGrounded = CheckPlayerCollision(-Vector3.up, 1.2f, 1 << 9 );
            if (centreMassGrounded)
            {
                stickToGroundForce = -10;
            }
            else
            {
                stickToGroundForce = 0;
            }
        }
        else
        {
            stickToGroundForce = 0;
            ySpeed -= gravity*Time.deltaTime;
        }
    }

    private void Rotate()
    {
        if ((inputDirection < 0 && facingRight) || (inputDirection > 0 && !facingRight))
        {
            facingRight = !facingRight;
            transform.Rotate(Vector3.up, 180);
        }   
    }

    private bool IsFacingWall()
    {
        if ((facingRight && wallToRight) || (!facingRight && wallToLeft)) return true;
        return false;
    }

    private bool NextToWall()
    {
        if (wallToLeft || wallToRight) return true;
        return false;
    }

    private bool CheckPlayerCollision(Vector3 direction, float length, int layerMask)
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position, direction * length, Color.green);
        if (Physics.Raycast(transform.position, direction, out hit, length, layerMask))
        {
            return true;
        }
        return false;
    }

    private bool HitCeiling()
    {
        return false;
    }
    
    private void Jump()
    {
        ySpeed = jumpForce;
        //Debug.Log(ySpeed);
    }

    private void WallJump()
    {
        Vector3 dir = Vector3.zero;
        dir.y = 1;
        if (wallToRight)
        {
            Debug.Log("Wall to right");
            // add impact top right
            dir.x = -1; 
        }
        else
        {
            Debug.Log("Wall to left");
            dir.x = 1;
        }
        AddImpact(dir);
    }

    private void AddImpact(Vector3 direction)
    {
        direction.Normalize();
        impactVector += direction.normalized * wallJumpForce;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Passthrough") 
        {
            passPlatform = other.gameObject.GetComponent<Collider>();
        }
    }
}