using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using Sirenix.OdinInspector;

public class TMProScreenMessage : Singleton<TMProScreenMessage>
{
    public TextMeshProUGUI text;
    private Vector3 originalScale;

    private void Start()
    {
        originalScale = transform.localScale;   
    }

    [Button("ShowMessage")]
    public void AppearMessage(string message = "", float scale = 1, Color color = new Color(), float duration = 2.0f)
    {
        text.text = message;
        text.color = Color.clear;

        transform.localScale = Vector3.one * scale;

        transform.DORewind();
        text.DORewind();

        transform.DOScale(scale * 1.25f, duration);
        text.DOColor(color, duration / 2).OnComplete(() => 
        {
            text.DOColor(Color.clear, duration / 2);
        });
    }
}