using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour {

    public Camera playerCam;
    public GameObject arrowPrefab;
    public Transform arrowSpawn;    
    public Slider chargeSlider;

    public float speed = 20f;
    public float chargeMax;
    public float chargeSpeedRate;    

    float chargeCurrent;

	void Update ()
    {        
        if (Input.GetMouseButton(1) && chargeCurrent < chargeMax)
        {
            chargeCurrent += Time.deltaTime * chargeSpeedRate;
            chargeSlider.value = chargeCurrent;
        }

        if (Input.GetMouseButtonUp(1))
        {
            GameObject newArrow = Instantiate(arrowPrefab, arrowSpawn.position, Quaternion.identity);

            Rigidbody rb = newArrow.GetComponent<Rigidbody>();
            rb.velocity = playerCam.transform.forward * chargeCurrent * speed;

            chargeCurrent = 0f;
            chargeSlider.value = chargeCurrent;
        }
	}
}