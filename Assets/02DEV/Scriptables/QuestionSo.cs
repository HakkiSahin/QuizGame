using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestionSo", menuName = "Scriptable Objects/QuestionSo")]
public class QuestionSo : ScriptableObject
{
    public string questionIndex;
    public List<Sprite> questionIcons;
    
    public string answerString;
    public List<string> othersString;
}
