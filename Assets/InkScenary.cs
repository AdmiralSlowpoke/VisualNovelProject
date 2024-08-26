using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.UI;
using TMPro;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Ink;
using System.Linq;

public class InkScenary : MonoBehaviour
{
    // Start is called before the first frame update
    public bool debugMode = false;
    public CharacterSOContainer left, right;
    public Button prefabButton;
    public GameObject buttonLocation;
    public Story story;
    public TextMeshProUGUI charNameText, charText;
    public CharacterSO Bakula, Chert, Chub, Dyak, Golova, Oksana, Pacuk, Soloha, Sverbiguz;
    [SerializeField]
    private TextAsset text;
    private bool textRunning=false;
    public static class AllData
    {
        public static Dictionary<string, CharacterSO> characters = new Dictionary<string, CharacterSO>();
        public static Dictionary<string, BackGroundSO> backgrounds = new Dictionary<string, BackGroundSO>();
        public static bool clickWait = false;
        public static TextMeshProUGUI charNameText;
        public static TextMeshProUGUI charText;
        public static List<GameObject> charactersImages = new List<GameObject>();
        public static Image backgroundImage;
        public static AudioSource music;
        public static AudioSource sfx;

    }
    public void ResetAllData()
    {
        AllData.characters = new Dictionary<string, CharacterSO>();
        AllData.backgrounds = new Dictionary<string, BackGroundSO>();
        AllData.clickWait = false;
        AllData.charNameText = null;
        AllData.charText = null;
        AllData.charactersImages = new List<GameObject>();
        AllData.backgroundImage = null;
        AllData.music = null;
        AllData.sfx = null;
    }
    public void SaveData()
    {
        StopAllCoroutines();
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath
    + "/MySaveData.dat");
       
        SavedData data = new SavedData();
        if (AllData.charactersImages[0].activeSelf)
        {
            data.firstCharacter = AllData.charactersImages[0].GetComponent<CharacterSOContainer>().characterContainer.name;
        }
        else data.firstCharacter = "null";
        if (AllData.charactersImages[1].activeSelf)
        {
            data.secondCharacter = AllData.charactersImages[1].GetComponent<CharacterSOContainer>().characterContainer.name;
        }
        else data.secondCharacter = "null";
        data.backgroundImage = AllData.backgroundImage.sprite.name;
        data.storyState = story.state.ToJson();
        bf.Serialize(file, data);
        file.Close();
        PlayerPrefs.SetInt("Saved", 1);
    }
    public void LoadSavedData()
    {
        if (File.Exists(Application.persistentDataPath + "/MySaveData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/MySaveData.dat", FileMode.Open);
            SavedData data = (SavedData)bf.Deserialize(file);
            file.Close();
            if (data.firstCharacter != "null")
            {
                CharacterEnter(AllData.characters[data.firstCharacter]);
            }
            if (data.secondCharacter != "null")
            {
                CharacterEnter(AllData.characters[data.secondCharacter]);
            }
            StopAllCoroutines();
            AllData.backgroundImage.sprite = Resources.Load<Sprite>($"Backgrounds/{data.backgroundImage}");
            story = new Story(text.text);
            story.state.LoadJson(data.storyState);
            StartCoroutine(AppearingText(story.currentText));
        }
    }
    public void LoadData()
    {
        var allCharacters = Resources.LoadAll("Characters", typeof(CharacterSO));
        foreach (CharacterSO character in allCharacters)
        {
            AllData.characters.Add(character.name, character);
        }
        var allBackgrounds = Resources.LoadAll("Backgrounds", typeof(BackGroundSO));
        foreach (BackGroundSO background in allBackgrounds)
        {
            AllData.backgrounds.Add(background.backgroundName, background);
        }
        AllData.charNameText = GameObject.Find("CharName").GetComponent<TextMeshProUGUI>();
        AllData.charText = GameObject.Find("CharText").GetComponentInChildren<TextMeshProUGUI>();
        AllData.charactersImages.Add(GameObject.Find("Character"));
        AllData.charactersImages.Add(GameObject.Find("Character1"));
        foreach (GameObject character in AllData.charactersImages)
        {
            character.SetActive(false);
        }
        AllData.backgroundImage = GameObject.Find("Background").GetComponent<Image>();
        AllData.music = Camera.main.GetComponent<AudioSource>();
        AllData.sfx = GameObject.Find("SFXSource").GetComponent<AudioSource>();
    }
    void Start()
    {
        EscMenu.GameIsPaused = false;
        Time.timeScale = 1f;
        LoadData();
        if (PlayerPrefs.GetInt("Saved") == 1)
        {
            LoadSavedData();
        }
        else LoadStory();
    }
    public void LoadStory()
    {
        StopAllCoroutines();
        story = new Story(text.text);
        Action();
    }
    public void Action()
    {
        string text = story.Continue();
        Debug.Log(text);
        text = text.Trim();
        StartCoroutine(AppearingText(text));
        if (story.currentTags.Count > 0) HandleTags(story.currentTags);
        if (story.currentChoices.Count > 0)
        {
            for (int i = 0; i < story.currentChoices.Count; i++)
            {
                Choice choice = story.currentChoices[i];
                Button button = Instantiate(prefabButton) as Button;
                button.transform.SetParent(buttonLocation.transform);
                button.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height*0.1f);
                //
                button.GetComponentInChildren<TMP_Text>().text = choice.text;
                button.onClick.AddListener(delegate
                {
                    OnClickChoiseButton(choice);
                });
            }
        }
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log($"EscMenuPaused: {EscMenu.GameIsPaused} storyCanContinue: {story.canContinue} textRunning: {textRunning}");
        }
        if (Input.GetMouseButtonDown(0) && !EscMenu.GameIsPaused && story.canContinue && !textRunning) Action();
        else if (Input.GetMouseButtonDown(0) && textRunning)
        {
            StopAllCoroutines();
            AllData.charText.text = story.currentText.Trim();
            textRunning = false;
        }
    }
    void OnClickChoiseButton(Choice choice)
    {
        story.ChooseChoiceIndex(choice.index);
        DeleteButtons();
        AllData.charText.text = choice.text;
        AllData.charNameText.text = "Вакула";
    }
    void DeleteButtons()
    {
        foreach (Button btn in buttonLocation.transform.GetComponentsInChildren<Button>())
        {
            Destroy(btn.gameObject);
        }
    }
    void HandleTags(List<string> currentTags)
    {
        string speakerTag = "";
        string emotionTag = "";
        string soundTag = "";
        string enterTag = "";
        string leaveTag = "";
        string backgroundTag = "";
        foreach(string tag in currentTags)
        {
            switch (tag)
            {
                case string a when a.Contains("speaker"):
                    speakerTag = tag.Replace("speaker: ", "");
                    AllData.charNameText.text = AllData.characters[speakerTag].characterName;
                    if (AllData.characters[speakerTag].characterName== "Рассказчик") AllData.charNameText.text = "";
                    if (debugMode) Debug.Log($"Персонаж {speakerTag}");
                    break;
                case string a when a.Contains("emotion"):
                    emotionTag = tag.Replace("emotion: ", "");
                    CharacterChangeMood(AllData.characters[speakerTag], emotionTag);
                    if (debugMode) Debug.Log($"Эмоция {emotionTag}");
                    break;
                case string a when a.Contains("звук"):
                    soundTag = tag.Replace("звук: ","");
                    AllData.sfx.PlayOneShot(Resources.Load<AudioClip>($"Audio/{soundTag}"));
                    if (debugMode) Debug.Log($"Звук {soundTag}");
                    break;
                case string a when a.Contains("enter"):
                    enterTag= tag.Replace("enter: ", "");
                    CharacterEnter(AllData.characters[enterTag]);
                    if (debugMode) Debug.Log($"Вошел {enterTag}");
                    break;
                case string a when a.Contains("leave"):
                    leaveTag = tag.Replace("leave: ", "");
                    CharacterLeave(AllData.characters[leaveTag]);
                    if (debugMode) Debug.Log($"Вышел {leaveTag}");
                    break;
                case string a when a.Contains("фон"):
                    backgroundTag = tag.Replace("фон: ", "");
                    AllData.backgroundImage.sprite = Resources.Load<Sprite>($"Backgrounds/{backgroundTag}");
                    break;

            }
        }
    }

    private void CharacterEnter(CharacterSO character)
    {
        foreach (GameObject image in AllData.charactersImages)
        {
            if (!image.activeSelf)
            {
                image.SetActive(true);
                image.GetComponent<CharacterSOContainer>().characterContainer = character;
                image.GetComponent<RectTransform>().sizeDelta = character.characterSize;
                image.GetComponent<Image>().sprite = character.characterMoods.Find(x=>x.characterReactionName=="IDLE").characterReactionImage;
                if (character.characterName == "Чуб")
                {
                    RectTransform rect = image.GetComponent<RectTransform>();
                    rect.transform.localPosition = new Vector3(-780, -903, 0);
                }
                else if (character.characterName == "Вакула")
                {
                    RectTransform rect = image.GetComponent<RectTransform>();
                    rect.transform.localPosition = new Vector3(-840, -650, 0);
                }
                else if (character.characterName == "Голова")
                {
                    RectTransform rect = image.GetComponent<RectTransform>();
                    rect.transform.localPosition = new Vector3(-840, -696, 0);
                }
                else if (character.characterName == "Дьяк")
                {
                    RectTransform rect = image.GetComponent<RectTransform>();
                    rect.transform.localPosition = new Vector3(-960, -550, 0);
                }
                break;
            }
        }
    }
    private void CharacterSpeak(CharacterSO character)
    {
        foreach(GameObject image in AllData.charactersImages)
        {
            if (image.activeSelf && image.GetComponent<CharacterSO>().characterName == character.characterName)
            {
                image.GetComponent<Image>().sprite= character.characterMoods.Find(x => x.characterReactionName == "Talk").characterReactionImage;
            }
        }
    }
    private void CharacterChangeMood(CharacterSO character, string mood)
    {
        foreach(GameObject obj in AllData.charactersImages)
        {
            if (obj.activeSelf)
            {
                if (obj.GetComponent<CharacterSOContainer>().characterContainer.characterName == character.characterName)
                {
                    obj.GetComponent<CharacterSOContainer>().SetCharacterExpression(mood);
                }
            }
        }
    }
    private void CharacterLeave(CharacterSO character)
    {
        if(debugMode)Debug.Log($"Выход{character.characterName}");
        foreach (GameObject obj in AllData.charactersImages)
        {
            if (debugMode) Debug.Log(obj.GetComponent<CharacterSOContainer>().characterContainer.characterName);
        }
        GameObject charImage = AllData.charactersImages.Find(x => x.GetComponent<CharacterSOContainer>().characterContainer.characterName == character.characterName);
        charImage.SetActive(false);
    }
    IEnumerator AppearingText(string text)
    {
        string temp = "";
        textRunning = true;
        for (int i = 0; i < text.Length; i++)
        {
            temp += text[i];
            AllData.charText.text = temp;
            yield return new WaitForSeconds(SettingsScript.textSpeed);
        }
        textRunning = false;
    }

}
/*
 * Нужно сохранять следующую инфу чтобы ничего не сломалось
 * Спрайты и инфу внутри них
 * Текущий текст либо текущую позицию
 * Задний фон
 */
[Serializable]
public class SavedData
{
    public string firstCharacter, secondCharacter;
    public string backgroundImage;
    public string storyState;
}