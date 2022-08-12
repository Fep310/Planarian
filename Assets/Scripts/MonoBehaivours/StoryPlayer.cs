using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryPlayer : MonoBehaviour
{
    [SerializeField] private ImageFader imageFader;
    [SerializeField] private TextAnimator textAnimator;
    [Space]
    [SerializeField] private Story story;

    public bool playerChoseAlternativeThisFrame;
    public int alternativeChosenId;

    private Chapter currentChapter;
    private ChapterEvent currentEvent;
    private EventTransitionData currentTransitionData;

    private void Start()
    {
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

            string onEndEventKey;

            if (currentEvent is ChapterQuestionEvent questionEvent)
            {
                // TODO: Display alternatives (create QuestionDisplayer)

                while (!playerChoseAlternativeThisFrame)
                    yield return null;

                playerChoseAlternativeThisFrame = false;

                onEndEventKey = questionEvent.Alternatives[alternativeChosenId].onChoose.Key;
            }
            else
            {
                onEndEventKey = currentEvent.OnEndEvent.Key;
            }

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

            while (!Input.GetKeyDown(KeyCode.Space))
                yield return null;
        }
    }
}
