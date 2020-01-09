using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public CharacterController controller;
    public Transform groundCheck;
    public LayerMask groundMask;
    public AudioSource audioSource;

    public float speed = 12f;    
    public float gravity = -9.81f;
    public float groundDistance = 0.4f;
    public float jumpForce = 3f;
    public float fallMultiplier = 2.5f;

    float x;
    float z;
    bool isGrounded;
    bool touchingDeath;

    Vector3 move;    
    Vector3 velocity;
    Vector3 initialPosition;
    LayerMask deathMask;

    void Start()
    {
        deathMask = LayerMask.GetMask("Water");
        initialPosition = transform.position;
    }

    void Update()
    {
        if (touchingDeath)
            transform.position = initialPosition;

        if (x != 0f || z != 0f)
        {
            if (isGrounded)
            {
                if (!audioSource.isPlaying)
                    audioSource.Play();
            }            
        }
        else
            audioSource.Stop();

    }

    void FixedUpdate()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        touchingDeath = Physics.CheckSphere(groundCheck.position, groundDistance, deathMask);

        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        move = transform.right * x + transform.forward * z;

        if (isGrounded)       
            velocity.y = gravity * Time.fixedDeltaTime;       

        if (controller.enabled)
            controller.Move(move * speed * Time.fixedDeltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
            velocity.y = jumpForce * -1f * gravity;

        if (!isGrounded && velocity.y < 0)
            velocity.y += gravity * (fallMultiplier - 1) * Time.fixedDeltaTime;

        velocity.y += gravity * Time.fixedDeltaTime;

        if (controller.enabled)
            controller.Move(velocity * Time.fixedDeltaTime);
    }
}
