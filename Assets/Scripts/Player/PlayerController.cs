using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float gravity = 0f;

    public float YSpeed { get; private set; }
    public float XSpeed { get; private set; }

    public bool enableGravity = false;

    private CharacterController controller;
    private Vector3 velocity = Vector3.zero;


    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (enableGravity)
            YSpeed = CalculateGravity();
        velocity = new Vector3(XSpeed, YSpeed, 0);
        controller.Move(velocity);      
    }

    public void Jump(float jumpForce)
    {
        YSpeed = jumpForce;
    }

    public void Move(Vector3 dir)
    {
        XSpeed = dir.x;
        YSpeed = dir.y;
    }

    public void Stop()
    {
        // TODO: add deceleration
        XSpeed = 0;
    }

    public float CalculateGravity()
    {
        // if character has fallen
        if (controller.isGrounded && YSpeed <= 0)
        {
            return -gravity / 100;
            /*centreMassGrounded = CheckPlayerCollision(-Vector3.up, 1.2f, 1 << 9 | 11);
            
            if (centreMassGrounded)
            {
                stickToGroundForce = -10;
            }
            else
            {
                stickToGroundForce = 0;
            }
            */
        }
        //stickToGroundForce = 0;
        return YSpeed - gravity * Time.deltaTime;
    }

    public void DisableGravity()
    {
        YSpeed = 0;
        enableGravity = false;
    }

    public void EnableGravity()
    {
        enableGravity = true;
    }

    public void ChangeSize(float size)
    {
        float sizeDifference = size - controller.height;
        Vector3 newCenter = new Vector3(controller.center.x, controller.center.y - sizeDifference/2, controller.center.z);
        controller.height = size;
        controller.center = newCenter;
    }
}
