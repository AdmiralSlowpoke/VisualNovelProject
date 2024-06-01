#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class StoryBoardEditor : EditorWindow
{
    [MenuItem("Tools/Story Board Editor")]
    public static void ShowStoryBoardEditor()
    {
        EditorWindow editorWindow = GetWindow<StoryBoardEditor>();
        editorWindow.titleContent = new GUIContent("Story Board Editor");
    }
    List<CharacterSO> charList = new List<CharacterSO>();
    List<BackGroundSO> backgroundList = new List<BackGroundSO>();
    private void FillList()
    {
        charList.Clear();
        backgroundList.Clear();
        var allCharacters = AssetDatabase.FindAssets("t:" + typeof(CharacterSO).Name);
        var allBackgrounds = AssetDatabase.FindAssets("t:" + typeof(BackGroundSO).Name);

        foreach (var guid in allCharacters)
        {
            charList.Add(AssetDatabase.LoadAssetAtPath<CharacterSO>(AssetDatabase.GUIDToAssetPath(guid)));
        }
        foreach (var guid in allBackgrounds)
        {
            backgroundList.Add(AssetDatabase.LoadAssetAtPath<BackGroundSO>(AssetDatabase.GUIDToAssetPath(guid)));
        }
    }
    public void CreateGUI()
    {
        FillList();
        rootVisualElement.Add(new Label($"Найдено персонажей: {charList.Count}"));
        rootVisualElement.Add(new Label($"Найдено фонов:{backgroundList.Count}"));
        Button button = new Button();
        button.name = "button1";
        button.text = "Начать создание сценария";
        rootVisualElement.Add(button);
    }
}

#endif