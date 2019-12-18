using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Transform player;

    void Start()
    {
        transform.rotation = Quaternion.Euler(18f, 0f, 0f);
    }
 
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");        
        
        transform.rotation = Quaternion.Euler(0f, mouseX * 30f, 0f);
    }
}
