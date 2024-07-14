using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DictScript : MonoBehaviour
{
    public GameObject dict;
    bool IsOpen = false;

    private void Start()
    {
        dict.SetActive(false);
    }
    public void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.D) && !IsOpen)
        {
            dict.SetActive(true);
            IsOpen = true;
            Time.timeScale = 0f;
        }
        if ((Input.GetKeyDown(KeyCode.D)|| Input.GetKeyDown(KeyCode.Escape)) && !IsOpen)
        {
            dict.SetActive(false);
            IsOpen = false;
            Time.timeScale = 1f;
        }

        
        
    }
    public void OnClickDict()
    {
        dict.SetActive(true);
        IsOpen = true;
    }

    public void OnClickExitDict() 
    { 
        dict.SetActive(false);
    }
}
