using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowFire : MonoBehaviour {

    public float damagePerSecond = 5f;
    public int damageTimer = 20;
    
    SphereCollider spherePos;
    LayerMask burnMask;    

    bool burnable;    

	void Start ()
    {
        spherePos = GetComponentInChildren<SphereCollider>();
        burnMask = LayerMask.GetMask("Burnable");        
	}    

    void FixedUpdate()
    {    
        burnable = Physics.CheckSphere(spherePos.transform.position, 1f, burnMask);        
    }    

    private void OnTriggerEnter(Collider other)
    {
        if (burnable)
        {        
            if (other.GetComponent<DamageHandler>() != null)
                other.GetComponent<DamageHandler>().ApplyBurn(damageTimer, damagePerSecond);
        }            
    }
}
