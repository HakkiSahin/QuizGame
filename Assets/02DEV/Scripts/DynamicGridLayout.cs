using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using EventBus;
using Random = UnityEngine.Random;

public class DynamicGridLayout : MonoBehaviour
{
    [SerializeField] List<GameObject> fillAreas;
    
    private void OnEnable()
    {
        EventBus<LoadQuestionEvent>.AddListener(LoadQuestion);
        EventBus<ClearEvent>.AddListener(ClearDataList);
    }

    private void OnDisable()
    {
        EventBus<LoadQuestionEvent>.RemoveListener(LoadQuestion);
        EventBus<ClearEvent>.RemoveListener(ClearDataList);
    }

    private void LoadQuestion(object sender, LoadQuestionEvent e)
    {
        for (int i = 0; i < fillAreas.Count; i++)
        {
            if (i == e.Question.questionIcons.Count - 1)
            {
                fillAreas[i].SetActive(true);
            }
            else
            {
                fillAreas[i].SetActive(false);
            }
        }
      
        EventBus<FillAreaEvent>.Emit(this, new FillAreaEvent { SpriteList = e.Question.questionIcons });
    }
    
    
    private void ClearDataList(object sender, ClearEvent @event)
    {
        foreach (GameObject obj in fillAreas)
        {
            obj.SetActive(false);
        }
    }
    
}