using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject canvasSettings;

    public void Start()
    {
        canvasSettings.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            canvasSettings.SetActive(false);
        }
    }
    public void ClickOnNewGame()
    {
        PlayerPrefs.SetInt("Saved", 0);
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }
    public void ClickOnLoad()
    {
        if (PlayerPrefs.GetInt("Saved") != 0)
        {
            SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
        }
    }
    public void GoToScene(string sceneName) 
    {
        SceneManager.LoadScene(sceneName,LoadSceneMode.Single);
        
    }
    public void QuiteApp() 
    {
        Application.Quit();
        Debug.Log("Игра закрыта");
    }
    public void Awake()
    {
        canvasSettings = GameObject.Find("BGSettings");
    }

    public void OnClickSettings()
    {
      
        if(canvasSettings.activeSelf == false)
        {
            canvasSettings.SetActive(true);
        }
        else
        {
            canvasSettings.SetActive(false);
        }
        
    }
    


}
