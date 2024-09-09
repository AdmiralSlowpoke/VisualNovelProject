using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscMenuSnowRun : MonoBehaviour
{
    public static bool GameIsPaused = false;
    int variantExit;

    public GameObject pauseUI;
    public GameObject exitScreen;
    public GameObject helpScreen;



    private void Start()
    {

        pauseUI.SetActive(false);
        exitScreen.SetActive(false);
        helpScreen.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
             
            }
            else
            {
                Pause();
            }
            if (helpScreen.activeSelf == true)
            {
                helpScreen.SetActive(false);
            }
        }



    }
    private void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        pauseUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    public void Resume()
    {

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
       
    }



    public void OnClickHelpButton()
    {
        if (helpScreen.activeSelf == false)
        {
            helpScreen.SetActive(true);
        }
        else
        {
            helpScreen.SetActive(false);
        }
    }


    public void AgreeButtonMain()
    {
        if (variantExit == 1)
        {
           
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        }
        else
        {
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
