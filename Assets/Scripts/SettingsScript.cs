using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SettingsScript : MonoBehaviour
{
    public AudioMixer audioMixer;
    public void SetFullScreen() 
    {
        Screen.fullScreen = true;
    }

    public void OutFullScreen() 
    {
        Screen.fullScreen = false;
    }

    public void SetVolume(float volume) 
    {
       audioMixer.SetFloat("volume", volume);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name == "MainMenuSettings")
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
