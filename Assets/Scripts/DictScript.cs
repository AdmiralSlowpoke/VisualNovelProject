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
