using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SettingsScript : MonoBehaviour
{
    public AudioMixer audioMixer;
    public static float textSpeed=0.03f;
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
       Debug.Log(volume);
       audioMixer.SetFloat("MusicVolume", volume);
    }
    public void SetTextSpeed(float speed)
    {
        textSpeed = Mathf.Abs(speed);
    }
    
  
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name == "MainMenuSettings")
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

}
