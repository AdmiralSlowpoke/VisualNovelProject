using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestWindow : MonoBehaviour
{
    public Animator a;
    private void Start()
    {
        a.Play("Fade");
    }
  
}
