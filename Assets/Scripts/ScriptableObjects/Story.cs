using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Story : ScriptableObject
{
    [SerializeField] private List<Chapter> chapters;
    public List<Chapter> Chapters { get => chapters; }

    private int currentChapter;

    public Chapter Init() {
        currentChapter = 0;
        return chapters[currentChapter];
    }

    public Chapter GetNextChapter() {
        currentChapter++;
        return Chapters[currentChapter];
    }
}
