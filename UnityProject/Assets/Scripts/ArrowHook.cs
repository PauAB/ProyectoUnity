using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowHook : MonoBehaviour {
   
    public int maxDistance;
    public int travelSpeed;

    CharacterController playerController;

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

        playerController.Move(direction * travelSpeed * Time.deltaTime);

        float distance = Vector3.Distance(playerController.transform.position, transform.position);

        if (distance <= 0.1f)
        {
            isFlying = false;            
        }
    }       

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "DeathTrigger")
            isFlying = true;        
    }
}
