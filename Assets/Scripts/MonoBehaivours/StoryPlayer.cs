using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryPlayer : MonoBehaviour
{
    [SerializeField] private Image imageComp;
    [SerializeField] private TMPro.TextMeshPro textComp;

    [SerializeField] private Story story;

    private Chapter currentChapter;
    private ChapterEvent currentEvent;

    private IEnumerator Start()
    {
        if (imageComp == null || textComp == null || story == null)
        {
            Debug.LogError("Components not assigned on StoryPlayer.");
            yield break;
        }

        currentChapter = story.Init();
        currentEvent = currentChapter.BeginChapter();

        while (currentEvent != null)
        {
            yield return StartCoroutine(HandleCurrentEvent());
        }
    }

    private IEnumerator HandleCurrentEvent()
    {

        yield return null;
    }
}
