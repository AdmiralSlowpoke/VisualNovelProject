using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.UI;
using TMPro;

public class InkScenary : MonoBehaviour
{
    // Start is called before the first frame update
    public CharacterSOContainer left, right;
    public Button prefabButton;
    public GameObject buttonLocation;
    public Story story;
    public Text charNameText, charText;
    public CharacterSO Bakula, Chert, Chub, Dyak, Golova, Oksana, Pacuk, Soloha, Sverbiguz;
    [SerializeField]
    private TextAsset text;
    private bool textRunning=false;
    public static class AllData
    {
        public static Dictionary<string, CharacterSO> characters = new Dictionary<string, CharacterSO>();
        public static Dictionary<string, BackGroundSO> backgrounds = new Dictionary<string, BackGroundSO>();
        public static bool clickWait = false;
        public static Text charNameText;
        public static Text charText;
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
        AllData.charNameText = GameObject.Find("CharName").GetComponent<Text>();
        AllData.charText = GameObject.Find("CharText").GetComponentInChildren<Text>();
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
        if (Input.GetMouseButtonDown(0) && story.canContinue && !textRunning) Action();
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
        foreach(string tag in currentTags)
        {
            switch (tag)
            {
                case string a when a.Contains("speaker"):
                    speakerTag = tag.Replace("speaker: ", ""); ;
                    Debug.Log($"Персонаж {speakerTag}");
                    break;
                case string a when a.Contains("emotion"):
                    emotionTag = tag.Replace("emotion: ", "");
                    Debug.Log($"Эмоция {emotionTag}");
                    break;
                case string a when a.Contains("звук"):
                    soundTag = tag.Replace("звук: ","");
                    Debug.Log($"Звук {soundTag}");
                    break;
                case string a when a.Contains("enter"):
                    enterTag= tag.Replace("enter: ", "");
                    Debug.Log($"Вошел {enterTag}");
                    break;
                case string a when a.Contains("leave"):
                    leaveTag = tag.Replace("leave: ", "");
                    Debug.Log($"Вышел {leaveTag}");
                    break;
            }
        }
        if (speakerTag != "")
        {
            string character = speakerTag;
            AllData.charNameText.text = AllData.characters[character].characterName;
            CharacterSO characterSO= AllData.characters[character];
            CharacterMood charMood = emotionTag != "" ? characterSO.characterMoods.Find(x => x.characterReactionName == emotionTag) : characterSO.characterMoods.Find(x => x.characterReactionName == "IDLE");
            if (charMood != null)
            {
                AllData.charactersImages[0].SetActive(true);
                AllData.charactersImages[0].GetComponent<Image>().sprite = charMood.characterReactionImage;
            }
            else
            {
                AllData.charactersImages[0].SetActive(false);
            }
        }
    }
    IEnumerator AppearingText(string text)
    {
        string temp = "";
        textRunning = true;
        for (int i = 0; i < text.Length; i++)
        {
            temp += text[i];
            AllData.charText.text = temp;
            yield return new WaitForSeconds(0.03f);
        }
        textRunning = false;
    }

}
