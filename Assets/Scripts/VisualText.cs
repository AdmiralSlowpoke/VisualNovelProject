using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisualText : MonoBehaviour
{
    public float textSpeed=0.05f;
    public string text;
    public Text textUI;
    private string _text;
    private void Start()
    {
        StartCoroutine(TextShow());
    }
    private IEnumerator TextShow()
    {
        _text = "";
        for (int i = 0; i < text.Length; i++)
        {
            yield return new WaitForSeconds(textSpeed);
            _text += text[i];
            textUI.text = _text;
        }
    }
}
