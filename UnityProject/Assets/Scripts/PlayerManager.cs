using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour {

    public Camera playerCam;
    public GameObject arrowPrefab;
    public GameObject arrowFirePrefab;
    public GameObject arrowHookPrefab;
    public GameObject arrowSteelPrefab;
    public Transform arrowSpawn;
    public Slider chargeSlider;
    public Image arrowFireUI;
    public Image arrowHookUI;
    public Image arrowSteelUI;

    public float speed = 20f;
    public float chargeMax;
    public float chargeSpeedRate;
    public int maxArrowFire;
    public int maxArrowHook = 1;
    public int maxArrowSteel;

    float chargeCurrent;
    int currentArrowFire;
    int currentArrowHook;
    int currentArrowSteel;
    Color newColor;

    void Start()
    {
        newColor = arrowFireUI.color;
        newColor.a = 0.1f;

        arrowFireUI.color = newColor;
        arrowHookUI.color = newColor;
        arrowSteelUI.color = newColor;

        currentArrowFire = maxArrowFire;
        currentArrowHook = maxArrowHook;
        currentArrowSteel = maxArrowSteel;
    }

    void Update ()
    {        
        if (Input.GetMouseButton(1) && chargeCurrent < chargeMax)
        {
            chargeCurrent += Time.deltaTime * chargeSpeedRate;
            chargeSlider.value = chargeCurrent;
        }

        if (Input.GetMouseButtonUp(1))
        {
            if (arrowPrefab.name == "ArrowFire")
            {
                if (currentArrowFire > 0)
                {
                    ShootArrow();
                    currentArrowFire--;
                }
            }
            else if (arrowPrefab.name == "ArrowHook")
            {
                if (currentArrowHook > 0)
                {
                    ShootArrow();
                    currentArrowHook--;
                }
            }
            else if (arrowPrefab.name == "ArrowSteel")
            {
                if (currentArrowSteel > 0)
                {
                    ShootArrow();
                    currentArrowSteel--;
                }
            }
            else if (arrowPrefab.name == "Arrow")
                ShootArrow();

            chargeCurrent = 0f;
            chargeSlider.value = chargeCurrent;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))            
        {
            if (currentArrowFire > 0)
                arrowPrefab = arrowFirePrefab;

            newColor.a = 1f;
            arrowFireUI.color = newColor;
            newColor.a = 0.1f;
            arrowHookUI.color = newColor;
            arrowSteelUI.color = newColor;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (currentArrowHook > 0)
                arrowPrefab = arrowHookPrefab;

            newColor.a = 1f;
            arrowHookUI.color = newColor;
            newColor.a = 0.1f;
            arrowFireUI.color = newColor;
            arrowSteelUI.color = newColor;
        }            
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (currentArrowSteel > 0)
                arrowPrefab = arrowSteelPrefab;

            newColor.a = 1f;
            arrowSteelUI.color = newColor;
            newColor.a = 0.1f;
            arrowFireUI.color = newColor;
            arrowHookUI.color = newColor;
        }
    }

    void ShootArrow()
    {
        GameObject newArrow = Instantiate(arrowPrefab, arrowSpawn.position, Quaternion.identity);

        Rigidbody rb = newArrow.GetComponent<Rigidbody>();
        rb.velocity = playerCam.transform.forward * chargeCurrent * speed;        
    }
}