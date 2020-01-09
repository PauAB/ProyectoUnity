using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    public Scenes sceneManager;

    public float speed = 20f;
    public float chargeMax;
    public float chargeSpeedRate;    

    float chargeCurrent;    
    int currentArrowHook;
    int currentArrowFire;
    int currentArrowSteel;    
    int maxArrowHook;
    int maxArrowFire;
    int maxArrowSteel;
    bool touchingDeath;

    GameObject arrowPrefab;
    Color newColor;
    AudioSource audioSource;

    void Start()
    {
        Time.timeScale = 1f;

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

        if (SceneManager.GetActiveScene().name == "Gameplay")
            maxArrowHook = 2;
        else if (SceneManager.GetActiveScene().name == "FirstLevel")
            maxArrowHook = 3;

        if (SceneManager.GetActiveScene().name == "Gameplay")
            maxArrowFire = 3;
        else if (SceneManager.GetActiveScene().name == "FirstLevel")
            maxArrowFire = 3;

        if (SceneManager.GetActiveScene().name == "Gameplay")
            maxArrowSteel = 5;
        else if (SceneManager.GetActiveScene().name == "FirstLevel")
            maxArrowSteel = 7;        
    }

    void Update ()
    {
        if (arrowPrefab != null)
        {
            if (currentArrowHook >= maxArrowHook)
                currentArrowHook = maxArrowHook;

            if (currentArrowFire >= maxArrowFire)
                currentArrowFire = maxArrowFire;

            if (currentArrowSteel >= maxArrowSteel)
                currentArrowSteel = maxArrowSteel;

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
            if (currentArrowHook < maxArrowHook)
                currentArrowHook += amount;
            else if (currentArrowHook >= maxArrowHook)
                currentArrowHook = maxArrowHook;            

            arrowPrefab = arrowHookPrefab;

            newColor.a = 1f;
            selectedHookBgUI.color = newColor;

            newColor.a = 0f;
            selectedFireBgUI.color = newColor;
            selectedSteelBgUI.color = newColor;
        }
        else if (name == "ArrowFirePick(Clone)")
        {            
            if (currentArrowFire < maxArrowFire)            
                currentArrowFire += amount;                         
            else if (currentArrowFire >= maxArrowFire)            
                currentArrowFire = maxArrowFire;                                                     

            arrowPrefab = arrowFirePrefab;

            newColor.a = 1f;
            selectedFireBgUI.color = newColor;

            newColor.a = 0f;
            selectedHookBgUI.color = newColor;
            selectedSteelBgUI.color = newColor;
        }
        else if (name == "ArrowSteelPick(Clone)")
        {            
            if (currentArrowSteel < maxArrowSteel)
                currentArrowSteel += amount;
            else if (currentArrowSteel >= maxArrowSteel)
                currentArrowSteel = maxArrowSteel;            

            arrowPrefab = arrowSteelPrefab;

            newColor.a = 1f;
            selectedSteelBgUI.color = newColor;

            newColor.a = 0f;
            selectedHookBgUI.color = newColor;
            selectedFireBgUI.color = newColor;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "End")
        {
            if (SceneManager.GetActiveScene().name == "Gameplay")
                sceneManager.ChangeScene("FirstLevel");
            else if (SceneManager.GetActiveScene().name == "FirstLevel")
            {
                sceneManager.ChangeScene("Menu");
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }                
        }
    }
}