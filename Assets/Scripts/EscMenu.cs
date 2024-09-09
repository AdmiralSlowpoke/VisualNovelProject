using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public InkScenary save;
    public static bool IsOpen = false;
    int variantExit;

    public GameObject pauseUI;
    public GameObject dict;
    public GameObject settings;
    public GameObject load;
    public GameObject exitScreen;
    


    private void Start()
    {

        pauseUI.SetActive(false);
        dict.SetActive(false);
        settings.SetActive(false);
        load.SetActive(false);
        exitScreen.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused && dict.activeSelf == false && settings.activeSelf == false && load.activeSelf == false)
            {
                Resume();
            }
            else if(dict.activeSelf == false && settings.activeSelf == false && load.activeSelf == false)
            {
                Pause();
            }
            if (dict.activeSelf == true)
            {
                dict.SetActive(false);
                IsOpen = false;
                
            }
            if(settings.activeSelf == true)
            {
                settings.SetActive(false);
            }
            if (load.activeSelf == true)
            {
                load.SetActive(false);
            }
        }


        if (Input.GetKeyDown(KeyCode.D))
        {
            if (!IsOpen)
            {
                dict.SetActive(true);
                IsOpen = true;
                
            }
            else
            {
                dict.SetActive(false);
                IsOpen = false;
              
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
        if(dict.activeSelf == false)
        {
            IsOpen = true;
            dict.SetActive(true);
        }
        else
        {
            IsOpen = false;
            dict.SetActive(false);
           
        }
    }
    public void OnClickEscDict()
    {
        IsOpen = false;
        dict.SetActive(false);
      
    }

    public void OnClickSettings()
    {
        if (settings.activeSelf == false)
        {
            settings.SetActive(true);
        }
        else
        {
            settings.SetActive(false);
        }
    }
    public void OnClickSave()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
        pauseUI.SetActive(false);
    }
   

    public void AgreeButtonMain()
    {
        if(variantExit == 1)
        {
            save.SaveData();
            save.StopAllCoroutines();
            save.ResetAllData();
            SceneManager.LoadScene("MainMenu",LoadSceneMode.Single);
        }
        else
        {
            save.SaveData();
            save.StopAllCoroutines();
            save.ResetAllData();
            Application.Quit();
            Debug.Log("Игра закрыта");
        }
        
    }
    public void DisagreeButton()
    {
        exitScreen.SetActive(false);
    }
    public void QuitGame()
    {
        exitScreen.SetActive(true);
        variantExit = 0;

    }
    public void ExitMainMenu()
    {
        exitScreen.SetActive(true);
        variantExit = 1;
    }
}
