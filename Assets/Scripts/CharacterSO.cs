using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName ="Character",menuName ="VisualNovel/Character",order =2)]
public class CharacterSO : ScriptableObject
{
    public string characterName;
    public List<CharacterMood> characterMoods;
    public Vector2 characterSize;
    
}

[System.Serializable]
public class CharacterMood
{
    public string characterReactionName;
    public Sprite characterReactionImage;
}

