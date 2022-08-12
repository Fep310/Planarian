using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryPlayer : MonoBehaviour
{
    [SerializeField] private Image imageComp;
    [SerializeField] private TMPro.TextMeshPro textComp;
    [SerializeField] private ImageFader imageFader;
    [SerializeField] private TextAnimator textAnimator;
    [Space]
    [SerializeField] private Story story;

    private Chapter currentChapter;
    private ChapterEvent currentEvent;
    private EventTransitionData currentTransitionData;

    private void Start()
    {
        if (imageComp == null || textComp == null || story == null)
        {
            Debug.LogError("Components not assigned on StoryPlayer.");
            return;
        }

        currentChapter = story.Init();
        currentEvent = currentChapter.BeginChapter();
        currentTransitionData = currentEvent.TransitionData;

        StartCoroutine(HandleChapterEventsCo());
    }

    private IEnumerator HandleChapterEventsCo()
    {
        while (currentEvent != null)
        {
            if (currentTransitionData.Sprite != null)
                yield return StartCoroutine(FadeNewSpriteInCo());

            if (currentTransitionData.Texts != null || currentTransitionData.Texts.Count > 1)
                yield return StartCoroutine(ShowTextCo());

            while (!Input.GetKeyDown(KeyCode.Space))
                yield return null;

            string onEndEventKey = currentEvent.OnEndEvent.Key;
            currentEvent = currentChapter.GetChapterEvent(onEndEventKey);
        }

    }

    private IEnumerator FadeNewSpriteInCo()
    {
        yield return StartCoroutine(imageFader.FadeNewImageCo(currentTransitionData.Sprite, currentTransitionData.FadeInTime));
    }

    private IEnumerator ShowTextCo()
    {
        foreach (var text in currentTransitionData.Texts)
        {
            yield return StartCoroutine(textAnimator.AnimateTextCo(text, .1f));
        }
    }
}
