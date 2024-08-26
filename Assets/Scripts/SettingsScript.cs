using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SettingsScript : MonoBehaviour
{
    public AudioMixer audioMixer;
    public static float textSpeed=0.03f;
    private void Start()
    {
        
        if (PlayerPrefs.HasKey("Width"))
        {
            Screen.SetResolution(PlayerPrefs.GetInt("Width"), PlayerPrefs.GetInt("Height"),true);
        }
        if (PlayerPrefs.HasKey("FullScreen"))
        {
            if (PlayerPrefs.GetInt("FullScreen") == 1)
            {
                Screen.fullScreen = true;
            }
            else
                Screen.fullScreen = false;
        }
        if (PlayerPrefs.HasKey("MasterVolume"))
        {
            audioMixer.SetFloat("MasterVolume",PlayerPrefs.GetFloat("MasterVolume"));
        }
        if (PlayerPrefs.HasKey("TextSpeed"))
        {
            textSpeed = PlayerPrefs.GetFloat("TextSpeed");
        }
    }
    public void SetFullScreen() 
    {
        Screen.fullScreen = true;
        PlayerPrefs.SetInt("FullScreen", 1);
    }

    public void OutFullScreen() 
    {
        Screen.fullScreen = false;
        PlayerPrefs.SetInt("FullScreen", 0);
    }

    public void SetVolume(float volume) 
    {
        Debug.Log(volume);
        audioMixer.SetFloat("MasterVolume", volume);
        PlayerPrefs.SetFloat("MasterVolume", volume);
    }
    public void SetTextSpeed(float speed)
    {
        textSpeed = Mathf.Abs(speed);
        PlayerPrefs.SetFloat("TextSpeed", textSpeed);
    }
    
  
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name == "MainMenuSettings")
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

}
