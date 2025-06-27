using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class TypingText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textUI;
    private string _currText;
    [SerializeField] private float typingSpeed = 0.1f;

    void Awake()
    {
        _currText = textUI.text;
    }

    void OnEnable()
    {
        textUI.text = string.Empty;

        StartCoroutine(TypingCoroutine());
    }

    IEnumerator TypingCoroutine()
    {
        var wfs = new WaitForSeconds(typingSpeed);
        int textCount = _currText.Length;
        for (int i = 0; i < textCount; i++)
        {
            textUI.text += _currText[i];
            yield return wfs;
        }
    }
}
