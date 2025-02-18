using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using EventBus;
using Random = UnityEngine.Random;

public class DynamicGridLayout : MonoBehaviour
{
    [SerializeField] List<GameObject> fillAreas;

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
        fillAreas[e.Question.questionIcons.Count - 1].SetActive(true);
        EventBus<FillAreaEvent>.Emit(this, new FillAreaEvent { SpriteList = e.Question.questionIcons });
    }
    
}