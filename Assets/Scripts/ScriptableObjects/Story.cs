using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Story", menuName = "Story/New Story")]
public class Story : ScriptableObject
{
    [SerializeField] private List<Chapter> chapters;
    public List<Chapter> Chapters { get => chapters; }

    [SerializeField] private List<StoryValue> values;
    public List<StoryValue> Values { get => values; }

    private int currentChapter;

    public Chapter Init() {

        foreach (var v in values)
            v.ResetValue();

        currentChapter = 0;
        return chapters[currentChapter];
    }

    public Chapter GetNextChapter() {
        currentChapter++;
        return Chapters[currentChapter];
    }
}
