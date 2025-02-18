using System.Collections.Generic;
using UnityEngine;

public struct LoadQuestionEvent
{
    public QuestionSo Question;
}

public struct FillAreaEvent
{
    public List<Sprite> SpriteList;
}

public struct LoadNextQuestionEvent
{
}