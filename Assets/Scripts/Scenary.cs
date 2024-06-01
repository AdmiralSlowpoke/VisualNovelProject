using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using Ink.Runtime;
using UnityEngine.Audio;

public static class AllData
{
    public static Dictionary<string, CharacterSO> characters = new Dictionary<string, CharacterSO>();
    public static Dictionary<string, BackGroundSO> backgrounds = new Dictionary<string, BackGroundSO>();
    public static bool clickWait=false;
    public static Text charNameText;
    public static Text charText;
    public static List<GameObject> charactersImages = new List<GameObject>();
    public static Image backgroundImage;
    public static AudioSource music;
    public static AudioSource sfx;
    
}
public class Scenary : MonoBehaviour
{
    private int move = 0;
    public Story story;
    public TextAsset ink;
    private List<IAction> actions = new List<IAction>();
    public void LoadData()
    {
        var allCharacters = Resources.LoadAll("Characters", typeof(CharacterSO));
        foreach (CharacterSO character in allCharacters)
        {
            AllData.characters.Add(character.characterName, character);
        }
        var allBackgrounds = Resources.LoadAll("Backgrounds", typeof(BackGroundSO));
        foreach(BackGroundSO background in allBackgrounds)
        {
            AllData.backgrounds.Add(background.backgroundName, background);
        }
        AllData.charNameText= GameObject.Find("CharName").GetComponent<Text>();
        AllData.charText= GameObject.Find("CharText").GetComponentInChildren<Text>();
        AllData.charactersImages.Add(GameObject.Find("Character"));
        AllData.charactersImages.Add(GameObject.Find("Character1"));
        foreach(GameObject character in AllData.charactersImages)
        {
            character.SetActive(false);
        }
        AllData.backgroundImage = GameObject.Find("Background").GetComponent<Image>();
        AllData.music = Camera.main.GetComponent<AudioSource>();
        AllData.sfx = GameObject.Find("SFXSource").GetComponent<AudioSource>();
    }
    private void Action(TextAsset textAsset)
    {
        story = new Story(textAsset.text);
        string text = story.Continue();
        text = text.Trim();
        Debug.Log(text);
    }
    private void Start()
    {
        LoadData();
        Action(ink);
    }
    private void Update()
    {
        if (move < actions.Count)
        {
            if (!(actions[move] is Speak))
            {
                DoAction();
            }
            if (Input.GetMouseButtonDown(0) && !AllData.clickWait)
            {
                StopAllCoroutines();
                DoAction();
                StartCoroutine(ClickCooldown());
            }
        }
    }
    IEnumerator ClickCooldown()
    {
        AllData.clickWait = true;
        float waitTime = 0.5f;
        while (waitTime > 0)
        {
            waitTime -= Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        AllData.clickWait = false;
    }
    private void DoAction()
    {
        if (move < actions.Count)
        {
            actions[move].Action(this);
            move++;
        }
    }
}
public class IAction
{
    public virtual void Action(MonoBehaviour mono) { }
}
public class Enter : IAction
{
   CharacterSO character;
   public Enter(CharacterSO character)
   {
        this.character = character;
    }
   public override void Action(MonoBehaviour mono)
   {
        foreach(GameObject image in AllData.charactersImages)
        {
            if (image.activeSelf == false)
            {
                image.SetActive(true);
                image.GetComponent<CharacterSOContainer>().characterContainer = character;
                image.GetComponent<CharacterSOContainer>().SetCharacterExpression("Neutral");
                break;
            }
        }
   }
}
public class Leave : IAction
{
    CharacterSO character;
    public Leave(CharacterSO character)
    {
        this.character = character;
    }
    public override void Action(MonoBehaviour mono)
    {
        GameObject charImage = AllData.charactersImages.Find(x => x.GetComponent<CharacterSOContainer>().characterContainer.characterName == character.characterName);
        charImage.GetComponent<CharacterSOContainer>().characterContainer = null;
        charImage.SetActive(false);
    }
}
public class ChangeBackground : IAction
{
    BackGroundSO backGround;
    public ChangeBackground(BackGroundSO backGround)
    {
        this.backGround = backGround;
    }
    public override void Action(MonoBehaviour mono)
    {
        AllData.backgroundImage.sprite = backGround.backgroundImage;
        if (backGround.backgroundMusic != null)
        {
            AllData.music.PlayOneShot(backGround.backgroundMusic);
        }
    }
}
public class ChangeMood : IAction
{
    CharacterSO character;
    string charMood;
    public ChangeMood(CharacterSO character,string charMood)
    {
        this.character = character;
        this.charMood = charMood;
    }
    public override void Action(MonoBehaviour mono)
    {
        GameObject charImage = AllData.charactersImages.Find(x => x.GetComponent<CharacterSOContainer>().characterContainer.characterName == character.characterName);
        if (charImage != null)
        {
            charImage.GetComponent<CharacterSOContainer>().SetCharacterExpression(charMood);
        }
    }
}
public class Speak:IAction
{
    CharacterSO character;
    AudioClip audio;
    string text;
    string charMood;

    public Speak(CharacterSO character,string text,string charMood=null,AudioClip audio=null)
    {
        this.character = character;
        this.text = text;
        this.audio = audio;
        this.charMood = charMood;
    }
    public override void Action(MonoBehaviour mono)
    {
        AllData.charNameText.text = character.characterName;
        mono.StartCoroutine(TextShow(text));
    }
    IEnumerator TextShow(string text)
    {
        GameObject charImage = AllData.charactersImages.Find(x => x.GetComponent<CharacterSOContainer>().characterContainer.characterName==character.characterName);
        if (charImage != null)
        {
            charImage.GetComponent<Animator>().Play("GlowUp");
            if(charMood!=null) charImage.GetComponent<CharacterSOContainer>().SetCharacterExpression(charMood);
        }
        string baseText = "";
        for (int i = 0; i < text.Length; i++)
        {
            baseText += text[i];
            AllData.charText.text = baseText;
            yield return new WaitForSeconds(0.025f);
        }
        if (charImage != null)
        {
            charImage.GetComponent<Animator>().Play("GlowDown");
        }
    }
}
public class PlaySound : IAction
{
    AudioClip audio;
    public PlaySound(AudioClip audio)
    {
        this.audio = audio;
    }
    public override void Action(MonoBehaviour mono)
    {
        
    }
}