using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using EventBus;
using Random = UnityEngine.Random;

public class DynamicGridLayout : MonoBehaviour
{
    [SerializeField] List<GameObject> fillAreas;
    List<Sprite> spriteList = new();

    private readonly List<GameObject> _spawnedItems = new List<GameObject>();

    private void OnEnable()
    {
        EventBus<LoadQuestionEvent>.AddListener(LoadQuestion);
    }

    private void OnDisable()
    {
        EventBus<LoadQuestionEvent>.RemoveListener(LoadQuestion);
    }

    private void LoadQuestion(object sender, LoadQuestionEvent e)
    {
        spriteList.Clear();
        spriteList.AddRange(e.Question.questionIcons);
        FillList();
    }

    private void FillList()
    {
    }
}