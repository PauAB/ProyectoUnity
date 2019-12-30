using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public CharacterController controller;
    public Transform groundCheck;
    public LayerMask groundMask;

    public float speed = 12f;    
    public float gravity = -9.81f;
    public float groundDistance = 0.4f;
    public float jumpForce = 3f;
    public float fallMultiplier = 2.5f;

    float x;
    float z;
    bool isGrounded;

    Vector3 move;    
    Vector3 velocity;

    void FixedUpdate()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);                    

        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        move = transform.right * x + transform.forward * z;

        if (isGrounded)       
            velocity.y = gravity * Time.deltaTime;       

        if (controller.enabled)
            controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
            velocity.y = jumpForce * -1f * gravity;

        if (!isGrounded && velocity.y < 0)
            velocity.y += gravity * (fallMultiplier - 1) * Time.deltaTime;

        velocity.y += gravity * Time.deltaTime;

        if (controller.enabled)
            controller.Move(velocity * Time.deltaTime);
    }
}
