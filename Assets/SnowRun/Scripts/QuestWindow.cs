using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestWindow : MonoBehaviour
{
    public GameObject questWindow;
    public Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(ShowQuestWindow());

    }
    IEnumerator ShowQuestWindow()
    {
        animator.SetBool("Faded", false);
        yield return new WaitForSeconds(10f);
        
    }
}
