using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnimationScript : MonoBehaviour
{
    public Animator anim;
    private void Start()
    {
        anim.Play("Start");
    }
    public void FirstDialogue()
    {
        //TextMeshPro text=GameObject.Find("Canvas2").GetComponentInChildren<TextMeshPro>();
        //text.text = "Изволь, за такую цену готов быть твоим! У вас, я слышал, расписываются кровью; постой же, я достану в кармане гвоздь!.";
    }
}
