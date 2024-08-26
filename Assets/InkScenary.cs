using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class InkScenary : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator anim;
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
        LoadData();
        LoadStory();
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
                //
                button.GetComponentInChildren<Text>().text = choice.text;
                button.onClick.AddListener(delegate
                {
                    OnClickChoiseButton(choice);
                });
            }
        }
    }
    void Update()
    {
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
        //Camera.main.GetComponent<AudioSource>().PlayOneShot(clickSound);
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
        string transitTag = "";
        string gameTag = "";
        foreach (string tag in currentTags)
        {
            switch (tag)
            {
                case string a when a.Contains("speaker"):
                    speakerTag = tag.Replace("speaker: ", "");
                    AllData.charNameText.text = AllData.characters[speakerTag].characterName;
                    if (AllData.characters[speakerTag].characterName== "Рассказчик") AllData.charNameText.text = "";
                    //Debug.Log($"Персонаж {speakerTag}");
                    break;
                case string a when a.Contains("emotion"):
                    emotionTag = tag.Replace("emotion: ", "");
                    CharacterChangeMood(AllData.characters[speakerTag], emotionTag);
                    //Debug.Log($"Эмоция {emotionTag}");
                    break;
                case string a when a.Contains("звук"):
                    soundTag = tag.Replace("звук: ","");
                    AllData.sfx.PlayOneShot(Resources.Load<AudioClip>($"Audio/{soundTag}"));
                    //Debug.Log($"Звук {soundTag}");
                    break;
                case string a when a.Contains("enter"):
                    enterTag= tag.Replace("enter: ", "");
                    CharacterEnter(AllData.characters[enterTag]);
                    //Debug.Log($"Вошел {enterTag}");
                    break;
                case string a when a.Contains("leave"):
                    leaveTag = tag.Replace("leave: ", "");
                    CharacterLeave(AllData.characters[leaveTag]);
                    Debug.Log($"Вышел {leaveTag}");
                    break;
                case string a when a.Contains("фон"):
                    backgroundTag = tag.Replace("фон: ", "");
                    AllData.backgroundImage.sprite = Resources.Load<Sprite>($"Backgrounds/{backgroundTag}");
                    break;
                case string a when a.Contains("вставка"):
                    gameTag = tag.Replace("вставка", "");
                    anim.SetTrigger("AnimIsPlay");
                    SceneManager.LoadScene("SnowRun");
                    break;
                case string a when a.Contains("переход"):
                    anim.SetTrigger("AnimIsPlay");
                    break;

            }
        }
        /*
        if (backgroundTag != "")
        {
            Debug.Log($"Background/{backgroundTag}");
            AllData.backgroundImage.sprite = Resources.Load<Sprite>($"Backgrounds/{backgroundTag}");
        }
        if (soundTag != "")
        {
            Debug.Log($"Audio/{soundTag}");
            Debug.Log(Resources.Load<AudioClip>($"Audio/{soundTag}"));
            AllData.sfx.PlayOneShot(Resources.Load<AudioClip>($"Audio/{soundTag}"));
        }
        if (enterTag != "")
        {
            CharacterEnter(AllData.characters[enterTag]);
        }
        if (speakerTag != "")
        {
            AllData.charNameText.text = AllData.characters[speakerTag].characterName;
            //CharacterSpeak(AllData.characters[speakerTag]);
        }
        if (emotionTag != "")
        {
            Debug.Log($"{speakerTag}:{emotionTag}");
            CharacterChangeMood(AllData.characters[speakerTag], emotionTag);
        }
        if (leaveTag != "")
        {
            CharacterLeave(AllData.characters[leaveTag]);
        }*/
        /*if (speakerTag != "")
        {
            string character = speakerTag;
            AllData.charNameText.text = AllData.characters[character].characterName;
            CharacterSO characterSO= AllData.characters[character];
            CharacterMood charMood = emotionTag != "" ? characterSO.characterMoods.Find(x => x.characterReactionName == emotionTag) : characterSO.characterMoods.Find(x => x.characterReactionName == "IDLE");
            if (charMood != null)
            {
                Debug.Log(AllData.characters[character].characterSize);
                AllData.charactersImages[0].SetActive(true);
                AllData.charactersImages[0].GetComponent<Image>().sprite = charMood.characterReactionImage;
                AllData.charactersImages[0].GetComponent<RectTransform>().sizeDelta = AllData.characters[character].characterSize;
            }
            else
            {
                AllData.charactersImages[0].SetActive(false);
            }
        }*/
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
        Debug.Log($"Выход{character.characterName}");
        foreach (GameObject obj in AllData.charactersImages)
        {
            Debug.Log(obj.GetComponent<CharacterSOContainer>().characterContainer.characterName);
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
