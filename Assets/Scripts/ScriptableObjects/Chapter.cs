using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chapter : ScriptableObject
{
    [SerializeField] private string key;
    public string Key { get => key; }
    
    [SerializeField] private ChapterTrigger onBeginChapter;
    public ChapterTrigger OnBeginChapter { get => onBeginChapter; }

    
}
