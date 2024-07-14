using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    bool IsOpen = false;

    public GameObject pauseUI;
    public GameObject dict;
    public GameObject settings;
    


    private void Start()
    {

        pauseUI.SetActive(false);
        dict.SetActive(false);
        settings.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused && dict.activeSelf == false)
            {
                Resume();
            }
            else if(dict.activeSelf == false)
            {
                Pause();
            }
            if (dict.activeSelf == true)
            {
                dict.SetActive(false);
                IsOpen = false;
                Time.timeScale = 1f;
            }
        }


        if (Input.GetKeyDown(KeyCode.D))
        {
            if (!IsOpen)
            {
                dict.SetActive(true);
                IsOpen = true;
                Time.timeScale = 0f;
            }
            else
            {
                dict.SetActive(false);
                IsOpen = false;
                Time.timeScale = 1f;
            }
        }
       
      

    }
    private void Pause()
    {
        pauseUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    public void Resume()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void OnClickDict()
    {
        dict.SetActive(true);
        IsOpen = true;
    }

    public void OnClickExitDict()
    {
        dict.SetActive(false);
        IsOpen = false;
    }

    public void OnClickSettings()
    {
        settings.SetActive(true);
    }
    public void OnClickExitSettings()
    {
        settings.SetActive(false);
    }

}
