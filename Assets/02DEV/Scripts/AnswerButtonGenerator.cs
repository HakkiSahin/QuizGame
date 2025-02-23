using System.Collections.Generic;
using EventBus;
using UnityEngine;

public class AnswerButtonGenerator : MonoBehaviour
{
    [SerializeField] private Transform _parentTransform;
    [SerializeField] private int _buttonAmount;
    [SerializeField] private GameObject _buttonPrefab;

    private string _levelWord;
    
    private readonly List<GameObject> _buttons=new();
    private void OnEnable()
    {
        EventBus<CreateAnswerButtonEvent>.AddListener(CreateAnswerButton);
        EventBus<ClearEvent>.AddListener(ClearDataList);
    }

 

    private void OnDisable()
    {
        EventBus<CreateAnswerButtonEvent>.RemoveListener(CreateAnswerButton);
        EventBus<ClearEvent>.RemoveListener(ClearDataList);
    }


    private void CreateAnswerButton(object sender, CreateAnswerButtonEvent e)
    {
        _levelWord = e.LevelWord;
        Create();
    }

    private void Create()
    {
        string levelWord = GenerateShuffledLetters(_levelWord);

        foreach (var letter in levelWord)
        {
            GameObject buttonObj = Instantiate(_buttonPrefab, _parentTransform);
            _buttons.Add(buttonObj);
            buttonObj.GetComponent<LetterButton>().SetText(letter.ToString());
        }
    }


    private readonly char[] _letters = new char[24];

    private string GenerateShuffledLetters(string input)
    {
        string cleanedWord = input.Replace(" ", "");
        int wordLength = cleanedWord.Length;
        int remainingLetters = _buttonAmount - wordLength;

        if (remainingLetters < 0)
        {
            Debug.LogError("The word cannot be more than 28 letters!");
            return null;
        }

        
        for (int i = 0; i < wordLength; i++)
        {
            _letters[i] = cleanedWord[i];
        }
        for (int i = wordLength; i < _buttonAmount; i++)
        {
            _letters[i] = (char)Random.Range('A', 'Z' + 1);
        }

     
        FisherYatesShuffle();

        return new string(_letters);
    }

    private void FisherYatesShuffle()
    {
        for (int i = _letters.Length - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (_letters[i], _letters[j]) = (_letters[j], _letters[i]);
        }
    }
    
    private void ClearDataList(object sender, ClearEvent e)
    {
        foreach (var button in _buttons)
        {
            DestroyImmediate(button);
        }
        _buttons.Clear();
        
    }
}