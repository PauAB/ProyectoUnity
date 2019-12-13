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
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 targetDirection = transform.right * x + transform.forward * z;

        targetDirection = mainCamera.transform.TransformDirection(targetDirection);
        targetDirection.y = 0.0f;        

        transform.position += targetDirection * speed * Time.deltaTime;

        //transform.rotation = Quaternion.LookRotation(targetDirection, Vector3.up);

        if (Input.GetButtonDown("Jump") && CheckIsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    bool CheckIsGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(groundCheck.position, groundDistance);
    }
}
