using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResolutionSettings : MonoBehaviour
{
    [SerializeField] public TMP_Dropdown dropdownResolution;

    private Resolution[] resolutions;
    private List<Resolution> filteredResolutions;

    private float currentRefreshRate;
    private int currentResolutionIndex;
    void Start()
    {
        resolutions = Screen.resolutions;
        filteredResolutions = new List<Resolution>();

        dropdownResolution.ClearOptions();
        currentRefreshRate = Screen.currentResolution.refreshRate;

        for(int i=0; i<resolutions.Length; i++)
        {
            if (resolutions[i].refreshRate == currentRefreshRate)
            {
                filteredResolutions.Add(resolutions[i]);
            }
        }


        List<string> options = new List<string>();
        for(int i =0; i<filteredResolutions.Count; i++)
        {
            string resolutionOptions = filteredResolutions[i].width + "x" + filteredResolutions[i].height + " " + filteredResolutions[i].refreshRate + "Hz";
            options.Add(resolutionOptions);
            if (filteredResolutions[i].width == Screen.width && filteredResolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        dropdownResolution.AddOptions(options);
        dropdownResolution.value = currentResolutionIndex;
        dropdownResolution.RefreshShownValue();
        if (PlayerPrefs.HasKey("ResolutionIndex"))
        {
            dropdownResolution.value = PlayerPrefs.GetInt("ResolutionIndex");
        }
        if (PlayerPrefs.HasKey("Width"))
        {
            Screen.SetResolution(PlayerPrefs.GetInt("Width"), PlayerPrefs.GetInt("Height"), true);
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
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = filteredResolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, true);
        PlayerPrefs.SetInt("Width", resolution.width);
        PlayerPrefs.SetInt("Height", resolution.height);
        PlayerPrefs.SetInt("ResolutionIndex", resolutionIndex);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
