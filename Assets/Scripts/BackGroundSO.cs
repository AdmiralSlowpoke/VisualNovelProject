using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Background",menuName ="VisualNovel/BackGround",order =1)]
public class BackGroundSO : ScriptableObject
{
    public string backgroundName;
    public AudioClip backgroundMusic;
    public Sprite backgroundImage;
    
}
