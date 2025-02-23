using System;
using System.Collections.Generic;
using System.Globalization;
using EventBus;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class TextBoxGenerator : MonoBehaviour
{
    [SerializeField] List<GameObject> textObjects = new();
    [SerializeField] List<GameObject> createObject = new();
    [SerializeField] int maxLines = 8;

    private int _openLineIndex;
    private string _currentText = "";

    private List<GameObject> _createdAnswerObject = new();

    private void OnEnable()
    {
        EventBus<LoadQuestionEvent>.AddListener(LoadText);
        EventBus<ControlLetterEvent>.AddListener(ControlLetter);
        EventBus<ClearEvent>.AddListener(ClearDateList);
    }


    private void LoadText(object sender, LoadQuestionEvent e)
    {
        SetText(e.Question.answerString);
    }

    private void OnDisable()
    {
        EventBus<LoadQuestionEvent>.RemoveListener(LoadText);
        EventBus<ControlLetterEvent>.RemoveListener(ControlLetter);
        EventBus<ClearEvent>.RemoveListener(ClearDateList);
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
        
        foreach (var letter in _lastWord)
        {
            if (letter == ' ') _currentText += " ";
            else _currentText += "_";
        }

        EventBus<CreateAnswerButtonEvent>.Emit(this, new CreateAnswerButtonEvent { LevelWord = _lastWord });
    }

    private int ControlNextLine(string nextWord)
    {
        return _openLineIndex += _lastWord.Length + nextWord.Length > maxLines ? 1 : 0;
    }

    private void ControlLetter(object sender, ControlLetterEvent e)
    {
        
        Debug.Log(_lastWord);
        int index = _lastWord.IndexOf(e.Letter, StringComparison.Ordinal);

        if (index != -1)
        {
            _currentText = _currentText.Substring(0, index) + e.Letter + _currentText.Substring(index + 1);
            _lastWord = _lastWord.Substring(0, index) + "_" + _lastWord.Substring(index + 1);
            _createdAnswerObject[index].GetComponent<BoxController>().SetLetter(e.Letter);
            if (!_currentText.Contains("_"))
            {
                EventBus<LoadNextQuestionEvent>.Emit(this, new LoadNextQuestionEvent());
            }
        }
        else
        {
            Debug.Log("Wrong letter");
        }
    }

    private void ClearDateList(object sender, ClearEvent @event)
    {
        foreach (var btn in _createdAnswerObject)
        {
            DestroyImmediate(btn);
        }

        _lastWord = "";
        _currentText = "";
        _createdAnswerObject.Clear();
    }
}