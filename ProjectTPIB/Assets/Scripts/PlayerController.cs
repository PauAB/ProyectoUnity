using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    Transform groundCheck;
    [SerializeField]
    LayerMask groundMask;
    [SerializeField]
    Camera mainCamera;
    [SerializeField]
    Transform playerBody;

    public float speed;    
    public float groundDistance = 0.4f;
    public float jumpForce = 3f;
    public float mouseSensitivity;

    Rigidbody rb;
    Vector3 velocity;    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;        

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 targetDirection = transform.right * x + transform.forward * z;    
        targetDirection = mainCamera.transform.TransformDirection(targetDirection);        

        transform.position += targetDirection * speed * Time.deltaTime;

        playerBody.Rotate(Vector3.up * mouseX);

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }    

    bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    }    
}
