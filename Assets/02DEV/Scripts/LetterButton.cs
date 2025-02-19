using System;
using EventBus;
using TMPro;
using UnityEngine;

public class LetterButton : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMesh;

    

    public void SetLetter()
    {
        EventBus<ControlLetterEvent>.Emit(this, new ControlLetterEvent { Letter = textMesh.text });
    }

    public void SetText(string text)
    {
        textMesh.text = text;
    }
}
