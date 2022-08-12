using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New ChapterEvent", menuName = "Story/Chapter/New Event")]
public class ChapterEvent : ScriptableObject
{
    [SerializeField] private string key;
    public string Key { get => key; } 

    [SerializeField] private ChapterTrigger triggeredBy;
    public ChapterTrigger TriggeredBy { get => triggeredBy; }

    [SerializeField] private ChapterTrigger onEndEvent;
    public ChapterTrigger OnEndEvent { get => onEndEvent; }

    [SerializeField] private List<StoryCondition> eventConditions;
    public List<StoryCondition> EventConditions { get => eventConditions; }

    [Space]

    [SerializeField] private EventTransitionData eventOutcome;
    public EventTransitionData TransitionData { get => eventOutcome; }

    
}
