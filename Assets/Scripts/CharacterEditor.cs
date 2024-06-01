#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(CharacterSO))]
public class CharacterEditor : Editor
{
    CharacterSO character;
    private void OnEnable()
    {
        character = target as CharacterSO;
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (character.characterMoods.Count != 0)
        {
            for (int i = 0; i < character.characterMoods.Count; i++)
            {
                if (character.characterMoods[i].characterReactionImage != null)
                {
                    Texture2D texture = AssetPreview.GetAssetPreview(character.characterMoods[i].characterReactionImage);
                    GUILayout.Label("", GUILayout.Height(160), GUILayout.Width(140));
                    GUI.DrawTexture(GUILayoutUtility.GetLastRect(), texture);
                }
            }
        }
    }
}
#endif