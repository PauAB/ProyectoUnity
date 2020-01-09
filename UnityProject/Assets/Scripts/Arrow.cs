using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

    public float alpha = 1f;

    Rigidbody rb;
    AudioSource audioSource;

    float rayDistance = 3f;    

    [SerializeField]
    private float lifetime = 1f;
    [SerializeField]
    private float maxLifetime = 8f;
    private bool hitSomething = false;    

	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        transform.rotation = Quaternion.LookRotation(rb.velocity);
    }
		
	void Update ()
    {
        if (!hitSomething)
            transform.rotation = Quaternion.LookRotation(rb.velocity);

        RaycastHit hitInfo;

        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, rayDistance))
        {            
            Debug.DrawRay(transform.position, transform.forward, Color.green);            

            Vector3 dist = alpha * transform.position + alpha * hitInfo.point;

            if (dist.z <= 1.5f)
            {
                rb.constraints = RigidbodyConstraints.FreezeRotation;                
            }
        }

        Destroy(gameObject, maxLifetime);
	}

    private void OnCollisionEnter(Collision collision)
    {        
        if (collision.collider.tag != "Arrow" && collision.collider.tag != "Player"  && collision.collider.tag != "DeathTrigger")
        {
            audioSource.Play();

            hitSomething = true;
            
            if (collision.rigidbody != null)
                transform.parent = collision.transform;
            
            rb.isKinematic = true;

            Destroy(gameObject, lifetime);                       
        }        
    }
}