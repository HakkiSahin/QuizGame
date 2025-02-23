using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using EventBus;
using Random = UnityEngine.Random;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private string levelFolderName = "02DEV/Levels";

    public List<QuestionSo> selectedLevels = new List<QuestionSo>();

    private void OnEnable()
    {
        EventBus<LoadNextQuestionEvent>.AddListener(LoadNextQuestion);
    }

    private void OnDisable()
    {
        EventBus<LoadNextQuestionEvent>.RemoveListener(LoadNextQuestion);
    }

    void Start()
    {
        LoadLevels();
    }

    void LoadLevels()
    {
        QuestionSo[] allLevels = Resources.LoadAll<QuestionSo>(levelFolderName);

        if (allLevels.Length == 0)
        {
            Debug.LogError($"'{levelFolderName}' klasöründe hiç level bulunamadı!");
            return;
        }

        selectedLevels = allLevels.OrderBy(x => Random.value).Take(10).ToList();

        StartGame();
    }

    private void StartGame()
    {
        EventBus<LoadQuestionEvent>.Emit(this, new LoadQuestionEvent { Question = selectedLevels[0]});
    }

    private void LoadNextQuestion(object sender, LoadNextQuestionEvent @event)
    {
       
        selectedLevels.RemoveAt(0);
        EventBus<ClearEvent>.Emit(this, new ClearEvent());
        DOVirtual.DelayedCall(1f, StartGame);
    }

    [ContextMenu("Load Levels")]
    public void LoadNextLevels()
    {
        EventBus<LoadNextQuestionEvent>.Emit(this, new LoadNextQuestionEvent());
    }
}