using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowHook : MonoBehaviour {
   
    public int maxDistance;
    public int travelSpeed;

    CharacterController playerController;    
    Vector3 hookLocation;

    bool isFlying;

	void Start ()
    {
        playerController = FindObjectOfType<CharacterController>();
	}
		
	void Update ()
    {
        if (isFlying)
            MovePlayer();
	}    

    void MovePlayer()
    {
        Vector3 direction = transform.position - playerController.transform.position;
        playerController.transform.position += direction * travelSpeed * Time.deltaTime;

        float distance = Vector3.Distance(playerController.transform.position, hookLocation);

        if (distance <= 0.3f)
        {
            isFlying = false;
            playerController.enabled = true;
        }
    }       

    private void OnTriggerEnter(Collider other)
    {        
        hookLocation = transform.position;
        isFlying = true;
        playerController.enabled = false;
    }

    //private void OnDrawGizmos()
    //{
    //    Debug.DrawLine(playerController.transform.position, transform.position, Color.green);
    //}
}
