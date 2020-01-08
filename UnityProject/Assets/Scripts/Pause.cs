using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject pauseMenu;
    
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

            Time.timeScale = 1f;            
        }            
        else
        {
            pauseMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            Time.timeScale = 0f;            
        }
                    
        displayed = !displayed;        
    }

    public void Exit()
    {
        Application.Quit();
    }
}
