using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour {

    public Camera playerCam;    
    public GameObject arrowHookPrefab;
    public GameObject arrowFirePrefab;
    public GameObject arrowSteelPrefab;
    public Transform arrowSpawn;
    public Slider chargeSlider;    
    public Image arrowHookUI;
    public Image arrowFireUI;
    public Image arrowSteelUI;
    public Text arrowHookAmountUI;
    public Text arrowFireAmountUI;
    public Text arrowSteelAmountUI;
    public Image selectedHookBgUI;
    public Image selectedFireBgUI;
    public Image selectedSteelBgUI;

    public float speed = 20f;
    public float chargeMax;
    public float chargeSpeedRate;
    public int maxArrowFire;
    public int maxArrowHook = 1;
    public int maxArrowSteel;

    float chargeCurrent;    
    int currentArrowHook;
    int currentArrowFire;
    int currentArrowSteel;
    bool touchingDeath;

    GameObject arrowPrefab;
    Color newColor;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        newColor = arrowHookUI.color;
        newColor.a = 0.1f;

        arrowFireUI.color = newColor;
        arrowHookUI.color = newColor;
        arrowSteelUI.color = newColor;

        newColor.a = 0f;
        selectedHookBgUI.color = newColor;
        selectedFireBgUI.color = newColor;
        selectedSteelBgUI.color = newColor;
    }

    void Update ()
    {
        if (arrowPrefab != null)
        {
            if (currentArrowHook >= 2)
                currentArrowHook = 2;

            if (currentArrowFire >= 3)
                currentArrowFire = 3;

            if (currentArrowSteel >= 5)
                currentArrowSteel = 5;

            if (Input.GetMouseButton(1) && chargeCurrent < chargeMax
                && (currentArrowHook > 0 || currentArrowFire > 0 || currentArrowSteel > 0))
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

                chargeCurrent = 0f;
                chargeSlider.value = chargeCurrent;
            }        
        }

        SwitchArrow();
        ArrowUI();
    }

    void ShootArrow()
    {
        audioSource.Play();

        GameObject newArrow = Instantiate(arrowPrefab, arrowSpawn.position, Quaternion.identity);        

        Rigidbody rb = newArrow.GetComponent<Rigidbody>();
        rb.velocity = playerCam.transform.forward * chargeCurrent * speed;        
    }

    void SwitchArrow()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (currentArrowHook > 0)
            {
                arrowPrefab = arrowHookPrefab;

                newColor.a = 1f;
                selectedHookBgUI.color = newColor;

                newColor.a = 0f;
                selectedFireBgUI.color = newColor;
                selectedSteelBgUI.color = newColor;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (currentArrowFire > 0)
            {
                arrowPrefab = arrowFirePrefab;

                newColor.a = 1f;
                selectedFireBgUI.color = newColor;

                newColor.a = 0f;
                selectedHookBgUI.color = newColor;
                selectedSteelBgUI.color = newColor;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (currentArrowSteel > 0)
            {
                arrowPrefab = arrowSteelPrefab;

                newColor.a = 1f;
                selectedSteelBgUI.color = newColor;

                newColor.a = 0f;
                selectedHookBgUI.color = newColor;
                selectedFireBgUI.color = newColor;
            }            
        }
    }

    void ArrowUI()
    {
        if (currentArrowHook > 0)
        {
            newColor.a = 1f;
            arrowHookUI.color = newColor;
            arrowHookAmountUI.text = currentArrowHook.ToString();
        }            
        else if (currentArrowHook <= 0)
        {
            newColor.a = 0.1f;
            arrowHookUI.color = newColor;
            arrowHookAmountUI.text = currentArrowHook.ToString();

            newColor.a = 0f;
            selectedHookBgUI.color = newColor;
        }
        
        if (currentArrowFire > 0)
        {
            newColor.a = 1f;
            arrowFireUI.color = newColor;
            arrowFireAmountUI.text = currentArrowFire.ToString();
        }
        else if (currentArrowFire <= 0)
        {
            newColor.a = 0.1f;
            arrowFireUI.color = newColor;
            arrowFireAmountUI.text = currentArrowFire.ToString();

            newColor.a = 0f;
            selectedFireBgUI.color = newColor;
        }

        if (currentArrowSteel > 0)
        {
            newColor.a = 1f;
            arrowSteelUI.color = newColor;
            arrowSteelAmountUI.text = currentArrowSteel.ToString();
        }
        else if (currentArrowSteel <= 0)
        {
            newColor.a = 0.1f;
            arrowSteelUI.color = newColor;
            arrowSteelAmountUI.text = currentArrowSteel.ToString();

            newColor.a = 0f;
            selectedSteelBgUI.color = newColor;
        }
    }

    public void PickArrow(string name, int amount)
    {
        if (name == "ArrowHookPick(Clone)")
        {
            if (currentArrowHook < 2)
                currentArrowHook += amount;
            else if (currentArrowHook >= 2)
                currentArrowHook = 2;

            arrowPrefab = arrowHookPrefab;

            newColor.a = 1f;
            selectedHookBgUI.color = newColor;

            newColor.a = 0f;
            selectedFireBgUI.color = newColor;
            selectedSteelBgUI.color = newColor;
        }
        else if (name == "ArrowFirePick(Clone)")
        {
            if (currentArrowFire < 3)
                currentArrowFire += amount;
            else if (currentArrowFire >= 3)
                currentArrowFire = 3;

            arrowPrefab = arrowFirePrefab;

            newColor.a = 1f;
            selectedFireBgUI.color = newColor;

            newColor.a = 0f;
            selectedHookBgUI.color = newColor;
            selectedSteelBgUI.color = newColor;
        }
        else if (name == "ArrowSteelPick(Clone)")
        {
            if (currentArrowSteel < 5)
                currentArrowSteel += amount;
            else if (currentArrowSteel >= 5)
                currentArrowSteel = 5;

            arrowPrefab = arrowSteelPrefab;

            newColor.a = 1f;
            selectedSteelBgUI.color = newColor;

            newColor.a = 0f;
            selectedHookBgUI.color = newColor;
            selectedFireBgUI.color = newColor;
        }
    }    
}