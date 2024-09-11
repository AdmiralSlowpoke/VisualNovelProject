using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestWindow : MonoBehaviour
{

    public GameObject QuestWindowPrefab;
    public TextMeshProUGUI Text;
    public GameObject MoveHelpText;
    public Animator animation;

    private void Start()
    {
        QuestWindowPrefab.SetActive(false);
        StartCoroutine("ShowMoveHelpText");

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("findCrowbar") )
        {
            QuestWindowPrefab.SetActive(true);
            animation.SetTrigger("TaskOn");
            StartCoroutine("Invis");
            Destroy(other);
        }
        else if(other.gameObject.CompareTag("findDevil") )
        {
            Text.text = "Найдите нечистого";
            RectTransform rect = Text.GetComponent<RectTransform>();
            rect.transform.localPosition = new Vector3(10,5);
            QuestWindowPrefab.SetActive(true);
            animation.SetTrigger("TaskOn");
            StartCoroutine("Invis");
            Destroy(other);
        }
       

    }
    IEnumerator Invis()
    {
        
        yield return new WaitForSeconds(4f);
        QuestWindowPrefab.SetActive(false);
       
    }

    IEnumerator ShowMoveHelpText()
    {
        
        yield return new WaitForSeconds(10f);
        MoveHelpText.SetActive(false);
    }
}
