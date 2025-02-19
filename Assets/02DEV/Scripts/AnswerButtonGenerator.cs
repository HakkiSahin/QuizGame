using EventBus;
using UnityEngine;

public class AnswerButtonGenerator : MonoBehaviour
{
    [SerializeField] private Transform _parentTransform;
    [SerializeField] private int _buttonAmount;
    [SerializeField] private GameObject _buttonPrefab;

    private string _levelWord;

    private void OnEnable()
    {
        EventBus<CreateAnswerButtonEvent>.AddListener(CreateAnswerButton);
    }

    private void OnDisable()
    {
        EventBus<CreateAnswerButtonEvent>.RemoveListener(CreateAnswerButton);
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
            buttonObj.GetComponent<LetterButton>().SetText(letter.ToString());
        }
    }


    private readonly char[] _letters = new char[28];

    private string GenerateShuffledLetters(string input)
    {
        string cleanedWord = input.Replace(" ", "");
        int wordLength = cleanedWord.Length;
        int remainingLetters = 28 - wordLength;

        if (remainingLetters < 0)
        {
            Debug.LogError("Kelime 28 harften büyük olamaz!");
            return null;
        }

        // Mevcut harfleri diziye kopyala
        for (int i = 0; i < wordLength; i++)
        {
            _letters[i] = cleanedWord[i];
        }

        // Eksik harfleri rastgele tamamla (UnityEngine.Random.Range kullanıldı)
        for (int i = wordLength; i < 28; i++)
        {
            _letters[i] = (char)Random.Range('A', 'Z' + 1);
        }

        // Fisher-Yates Shuffle ile karıştır
        FisherYatesShuffle();

        return new string(_letters);
    }

    private void FisherYatesShuffle()
    {
        for (int i = _letters.Length - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            char temp = _letters[i];
            _letters[i] = _letters[j];
            _letters[j] = temp;
        }
    }
}