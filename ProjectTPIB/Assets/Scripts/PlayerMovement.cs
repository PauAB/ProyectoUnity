using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform groundCheck;
    public LayerMask groundMask;

    public float speed = 12f;
    public float sprintMultiplier = 2f;
    public float gravity = -9.81f;
    public float groundDistance = 0.4f;
    public float jumpHeight = 3f;

    Vector3 velocity;    
    bool isGrounded;

    void Start()
    {
        
    }
    
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 targetDirection = new Vector3(x, 0f, z);
        targetDirection = Camera.main.transform.TransformDirection(targetDirection);
        targetDirection.y = 0.0f;

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
        

        Vector3 move = transform.right * x + transform.forward * z;       

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
   
        controller.Move(velocity * Time.deltaTime);

        if (Input.GetButton("Fire3") && isGrounded)
        {
            controller.Move(move * speed * sprintMultiplier * Time.deltaTime);
        }      
    }
}
