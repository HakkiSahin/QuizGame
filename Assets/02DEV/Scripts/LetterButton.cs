using System;
using EventBus;
using TMPro;
using UnityEngine;

public class LetterButton : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMesh;
    [SerializeField] GameObject body;


    public void SetLetter()
    {
        EventBus<ControlLetterEvent>.Emit(this, new ControlLetterEvent { Letter = textMesh.text });
        
        body.SetActive(false);
    }

    public void SetText(string text)
    {
        textMesh.text = text;
    }
}