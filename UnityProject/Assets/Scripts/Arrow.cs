﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {    

    Rigidbody rb;

    private float lifetime = 1f;
    private float maxLifetime = 8f;
    private bool hitSomething = false;    

	void Start ()
    {
        rb = GetComponent<Rigidbody>();        
        transform.rotation = Quaternion.LookRotation(rb.velocity);        
    }
		
	void Update ()
    {
        if (!hitSomething)
            transform.rotation = Quaternion.LookRotation(rb.velocity);

        Destroy(gameObject, maxLifetime);
	}

    private void OnCollisionEnter(Collision collision)
    {        
        if (collision.collider.tag != "Arrow" && collision.collider.tag != "Player")
        {
            hitSomething = true;
            
            if (collision.rigidbody != null)
                transform.parent = collision.transform;

            rb.constraints = RigidbodyConstraints.FreezeAll;

            Destroy(gameObject, lifetime);
        }        
    }
}