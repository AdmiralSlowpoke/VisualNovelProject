using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestWindow : MonoBehaviour
{

    public GameObject QuestWindowPrefab;
    public GameObject MoveHelpText;
    public Animator animation;

    private void Start()
    {
        QuestWindowPrefab.SetActive(false);
        StartCoroutine("ShowMoveHelpText");

    }
    private void OnTriggerEnter(Collider other)
    {
        QuestWindowPrefab.SetActive(true);
        animation.SetTrigger("TaskOn");
        StartCoroutine("Invis");

    }
    IEnumerator Invis()
    {
        
        
        yield return new WaitForSeconds(4f);
        QuestWindowPrefab.SetActive(false);
        Destroy(this.gameObject);
    }

    IEnumerator ShowMoveHelpText()
    {
        
        yield return new WaitForSeconds(4f);
        MoveHelpText.SetActive(false);
    }
}
