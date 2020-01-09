using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject pauseMenu;
    public Camera cam;
    public AudioSource[] audioSources;

    public float vol1;
    public float vol2;
    public float vol3;
    public float vol4;
    public float vol5;

    bool displayed;

    void Start()
    {        
        pauseMenu.SetActive(false);
        displayed = false;

        vol1 = audioSources[0].volume;
        vol2 = audioSources[1].volume;
        vol3 = audioSources[2].volume;
        vol4 = audioSources[3].volume;
        vol5 = audioSources[4].volume;
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
        if (displayed) // UNPAUSE
        {
            pauseMenu.SetActive(false);

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            audioSources[0].volume = vol1;
            audioSources[1].volume = vol2;
            audioSources[2].volume = vol3;
            audioSources[3].volume = vol4;
            audioSources[4].volume = vol5;

            Time.timeScale = 1f;
        }            
        else // PAUSE
        {
            pauseMenu.SetActive(true);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            foreach (AudioSource source in audioSources)
            {
                if (source.isPlaying)
                    source.volume = 0f;
            }

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
