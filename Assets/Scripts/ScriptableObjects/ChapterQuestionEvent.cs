using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New QuestionEvent", menuName = "Story/Chapter/New Question Event")]
public class ChapterQuestionEvent : ChapterEvent
{
    [SerializeField] private List<QuestionAlternative> alternatives;
    public List<QuestionAlternative> Alternatives { get => alternatives; }
}
