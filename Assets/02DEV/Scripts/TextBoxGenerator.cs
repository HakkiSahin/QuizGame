using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using EventBus;
using Unity.Mathematics;
using UnityEngine;

public class TextBoxGenerator : MonoBehaviour
{
    [SerializeField] List<GameObject> textObjects = new();
    [SerializeField] List<GameObject> createObject = new();
    [SerializeField] int maxLines = 8;

    private int _openLineIndex;
    private string _currentText="";

    private List<GameObject> _createdAnswerObject = new();

    private void OnEnable()
    {
        EventBus<LoadQuestionEvent>.AddListener(LoadText);
        EventBus<ControlLetterEvent>.AddListener(ControlLetter);
    }


    private void LoadText(object sender, LoadQuestionEvent e)
    {
        SetText(e.Question.answerString);
    }

    private void OnDisable()
    {
        EventBus<LoadQuestionEvent>.RemoveListener(LoadText);
        EventBus<ControlLetterEvent>.RemoveListener(ControlLetter);
    }

    private string _lastWord = "";

    private void SetText(string text)
    {
        string[] textArray = text.Split(' ');

        _openLineIndex = 0;

        foreach (string word in textArray)
        {
            if (_lastWord != "") _openLineIndex = ControlNextLine(word);
            
            textObjects[_openLineIndex].SetActive(true);
            Transform parentObject = textObjects[_openLineIndex].transform;

            foreach (var letter in word)
            {
                _lastWord += letter;
                _createdAnswerObject.Add(Instantiate(createObject[0], Vector3.zero, quaternion.identity, parentObject));
            }

            _createdAnswerObject.Add(Instantiate(createObject[1], Vector3.zero, quaternion.identity, parentObject));
            _lastWord += " ";
        }

        _lastWord = _lastWord.ToUpper(new CultureInfo("en-US"));
        EventBus<CreateAnswerButtonEvent>.Emit(this, new CreateAnswerButtonEvent{LevelWord = _lastWord});
    }

    private int ControlNextLine(string nextWord)
    {
        return _openLineIndex += _lastWord.Length + nextWord.Length > maxLines ? 1 : 0;
    }

    private void ControlLetter(object sender, ControlLetterEvent e)
    {
        if (_lastWord[_currentText.Length].ToString() == " ") _currentText += " ";

        if (_lastWord[_currentText.Length].ToString() == e.Letter)
        {
            _currentText += e.Letter;
            _createdAnswerObject[_currentText.Length-1].GetComponent<BoxController>().SetLetter(e.Letter);
        }
        else
        {
            Debug.Log("Wrong letter");
        }
    }
}