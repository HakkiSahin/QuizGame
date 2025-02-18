using System;
using System.Collections.Generic;
using EventBus;
using UnityEngine;
using UnityEngine.UI;

public class PictureArea : MonoBehaviour
{
    [SerializeField] List<Image> pictureArea;

    private void OnEnable()
    {
        EventBus<FillAreaEvent>.AddListener(SetImages);
    }
    
    private void OnDisable()
    {
        EventBus<FillAreaEvent>.RemoveListener(SetImages);
    }

    private void SetImages(object sender, FillAreaEvent e)
    {
        for (int i = 0; i < pictureArea.Count; i++)
        {
            pictureArea[i].sprite = e.SpriteList[i];
        }
    }
}
