#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BackGroundSO))]
public class BackGroundEditor : Editor
{
    BackGroundSO groundSO;
    private void OnEnable()
    {
        groundSO = target as BackGroundSO;
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (groundSO.backgroundImage == null) return;
        Texture2D texture = AssetPreview.GetAssetPreview(groundSO.backgroundImage);
        GUILayout.Label("", GUILayout.Height(90), GUILayout.Width(160));
        GUI.DrawTexture(GUILayoutUtility.GetLastRect(), texture);
    }
}
#endif