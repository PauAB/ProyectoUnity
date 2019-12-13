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

    public float speed;
    public float gravity = -9.81f;
    public float groundDistance = 0.4f;
    public float jumpHeight = 3f;

    Rigidbody rb;
    Vector3 velocity;    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 targetDirection = new Vector3(x, 0f, z);
        targetDirection = mainCamera.transform.TransformDirection(targetDirection);
        targetDirection.y = 0.0f;

        Vector3 newTargetDirection = transform.right * x + transform.forward * z;
        newTargetDirection = mainCamera.transform.TransformDirection(newTargetDirection);

        rb.MovePosition(newTargetDirection * speed * Time.deltaTime);

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);

        Quaternion newRotation = Quaternion.Lerp(rb.rotation, targetRotation, Time.deltaTime);

        rb.MoveRotation(newRotation);

        if (Input.GetButtonDown("Jump") && CheckIsGrounded())
        {
            Jump();
        }
    }

    void Movement(float horizontal, float vertical)
    {
        if (horizontal != 0f || vertical != 0f)
        {
            Rotation(horizontal, vertical);
        }
    }

    void Rotation(float horizontal, float vertical)
    {
        Vector3 targetDirection = new Vector3(horizontal, 0f, vertical);
        targetDirection = mainCamera.transform.TransformDirection(targetDirection);
        targetDirection.y = 0.0f;

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);

        Quaternion newRotation = Quaternion.Lerp(rb.rotation, targetRotation, Time.deltaTime);

        rb.MoveRotation(newRotation);
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
    }

    bool CheckIsGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    }
}
