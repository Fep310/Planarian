using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New ChapterEvent Asset", menuName = "Planarian Assets/Chapter/Event")]
public class ChapterEvent : ScriptableObject
{
    [SerializeField] private string key;
    public string Key { get => key; } 

    [SerializeField] private ChapterTrigger triggeredBy;
    public ChapterTrigger TriggeredBy { get => triggeredBy; }

    [SerializeField] private List<StoryCondition> eventConditions;
    public List<StoryCondition> EventConditions { get => eventConditions; }

    [SerializeField] private ChapterTrigger onEndEvent;
    public ChapterTrigger OnEndEvent { get => onEndEvent; }
}
