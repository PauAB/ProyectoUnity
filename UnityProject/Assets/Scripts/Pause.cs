using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject pauseMenu;
    public Camera cam;
    
    bool displayed;

    void Start()
    {        
        pauseMenu.SetActive(false);
        displayed = false;        
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            DisplayMenu();
        }            
    }

    void DisplayMenu()
    {
        if (displayed)
        {
            pauseMenu.SetActive(false);

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            //cam.cullingMask |= 1 << LayerMask.NameToLayer("");
            //cam.cullingMask &= ~(1 << LayerMask.NameToLayer("UI"));

            Time.timeScale = 1f;            
        }            
        else
        {
            pauseMenu.SetActive(true);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            //cam.cullingMask |= 1 << LayerMask.NameToLayer("UI");
            //cam.cullingMask &= ~(1 << LayerMask.NameToLayer("Gun"));

            Time.timeScale = 0f;            
        }

        cam.cullingMask ^= 1 << LayerMask.NameToLayer("UI");
        cam.cullingMask ^= 1 << LayerMask.NameToLayer("Gun");
                    
        displayed = !displayed;        
    }

    public void Exit()
    {
        Application.Quit();
    }
}
