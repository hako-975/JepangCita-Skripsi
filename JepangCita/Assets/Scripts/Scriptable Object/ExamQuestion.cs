using UnityEngine;
using System;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewExam", menuName = "Data/New Exam")]
[System.Serializable]
public class ExamQuestion : ScriptableObject
{
    [TextArea(2, 2)]
    public string question;
    public List<ExamData> examData = new List<ExamData>();
}

[Serializable]
public class ExamData
{
    public string answer;
    public bool isRightAnswer;
}
