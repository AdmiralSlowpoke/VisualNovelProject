using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject canvasSettings;
    public int canvasSettingOn;

    public void Start()
    {
        canvasSettingOn = 0;
        canvasSettings.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            
        }
    }

    public void GoToScene(string sceneName) 
    {
        SceneManager.LoadScene(sceneName);
        
    }
    public void QuiteApp() 
    {
        Application.Quit();
        Debug.Log("Игра закрыта");
    }
    public void Awake()
    {
        canvasSettings = GameObject.Find("SettingsCanvas");
    }

    public void OnClickSettings()
    {
        if (canvasSettingOn ==0 )
        {
            canvasSettings.SetActive(true);
        }
        else
        {
            canvasSettings.SetActive(false);
        }
        
    }
    


}
