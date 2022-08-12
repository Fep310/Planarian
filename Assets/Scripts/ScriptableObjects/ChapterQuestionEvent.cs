using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New ChapterQuestionEvent Asset", menuName = "Story/Chapter/New Question Event")]
public class ChapterQuestionEvent : ChapterEvent
{
    [System.Serializable]
    public struct QuestionAlternative
    {
        public string label;
        public ChapterTrigger onChoose;
    }

    [SerializeField] private List<QuestionAlternative> alternatives;
    public List<QuestionAlternative> Alternatives { get => alternatives; }
}
