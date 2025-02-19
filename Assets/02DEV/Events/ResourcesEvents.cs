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

public struct ControlLetterEvent
{
    public string Letter;
}

public struct CreateAnswerButtonEvent
{
    public string LevelWord;
}