using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Story", menuName = "Story/New Story")]
public class Story : ScriptableObject
{
    [SerializeField] private List<Chapter> chapters;
    public List<Chapter> Chapters { get => chapters; }

    public int CurrentChapterIndex;

    [SerializeField] private List<StoryValue> values;
    public List<StoryValue> Values { get => values; }

    public Chapter NewGame() {

        foreach (var v in values)
            v.ResetValue();

        CurrentChapterIndex = 0;
        return chapters[CurrentChapterIndex];
    }

    public Chapter GetCurrentChapter()
    {
        return chapters[CurrentChapterIndex];
    }

    public Chapter GetNextChapter() {
        CurrentChapterIndex++;
        return chapters[CurrentChapterIndex];
    }
}
