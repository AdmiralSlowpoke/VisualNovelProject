using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider volumeSlider, textSlider;
    public static float textSpeed=0.03f;
    private void Start()
    {

        if (PlayerPrefs.HasKey("MasterVolume"))
        {
            audioMixer.SetFloat("MasterVolume",PlayerPrefs.GetFloat("MasterVolume"));
            volumeSlider.value = PlayerPrefs.GetFloat("MasterVolume");
        }
        if (PlayerPrefs.HasKey("TextSpeed"))
        {
            textSpeed = PlayerPrefs.GetFloat("TextSpeed");
            Debug.Log(textSpeed);
            textSlider.value = PlayerPrefs.GetFloat("TextSlider");
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
        audioMixer.SetFloat("MasterVolume", volume);
        PlayerPrefs.SetFloat("MasterVolume", volume);
    }
    public void SetTextSpeed(float speed)
    {
        textSpeed = Mathf.Abs(speed);
        PlayerPrefs.SetFloat("TextSpeed", textSpeed);
        PlayerPrefs.SetFloat("TextSlider", speed);
    }
    
  
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name == "MainMenuSettings")
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

}
