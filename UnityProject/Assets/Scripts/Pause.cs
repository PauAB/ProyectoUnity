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

            Time.timeScale = 1f;            
        }            
        else
        {
            pauseMenu.SetActive(true);

            Time.timeScale = 0f;            
        }
                    
        displayed = !displayed;        
    }

    public void Exit()
    {
        Application.Quit();
    }
}
