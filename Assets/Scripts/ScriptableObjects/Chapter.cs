using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Chapter", menuName = "Story/Chapter/New Chapter")]
public class Chapter : ScriptableObject
{
    [SerializeField] private ChapterTrigger onBeginChapter;
    public ChapterTrigger OnBeginChapter { get => onBeginChapter; }

    [SerializeField] private List<ChapterEvent> events;
    public List<ChapterEvent> Events { get => events; }

    public ChapterEvent BeginChapter()
    {
        return GetChapterEvent(onBeginChapter.Key);
    }

    public ChapterEvent GetChapterEvent(string trigger) {
        
        var nextPossibleEvents = new List<ChapterEvent>(); 

        foreach (var evt in events) {

            if (evt.TriggeredBy.Key == trigger) {

                bool allConditionsMet = true;

                if (evt.EventConditions.Count > 1)
                {
                    foreach (var condition in evt.EventConditions)
                    {
                        if (!condition.IsConditionMet())
                        {
                            allConditionsMet = false;
                            break;
                        }
                    }
                }

                if (allConditionsMet)
                    nextPossibleEvents.Add(evt);
            }
        }
        
        if (nextPossibleEvents.Count < 1) {
            return null;
        }

        return nextPossibleEvents[0];
    }
}
