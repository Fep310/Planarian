using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryPlayer : InitializableMonoBehaviour
{
    [SerializeField] private Story story;
    [Space]
    [SerializeField] private ImageFader imageFader;
    [SerializeField] private TextAnimator textAnimator;
    [SerializeField] private QuestionDisplayer questionDisplayer;

    [HideInInspector] public bool playerChoseAlternativeThisFrame;
    [HideInInspector] public int alternativeChosenNumber;

    private Chapter currentChapter;
    private ChapterEvent currentEvent;
    private EventTransitionData currentTransitionData;

    private void OnEnable()
    {
        foreach (var btn in questionDisplayer.alternativeButtons)
            btn.onChoose += (i) => SetChosenAlternative(i);
    }

    private void OnDisable()
    {
        foreach (var btn in questionDisplayer.alternativeButtons)
            btn.onChoose -= (i) => SetChosenAlternative(i);
    }

    private void SetChosenAlternative(int number)
    {
        playerChoseAlternativeThisFrame = true;
        alternativeChosenNumber = number;
    }

    public override void Init()
    {
        currentChapter = story.Init();
        currentEvent = currentChapter.BeginChapter();

        StartCoroutine(HandleChapterEventsCo());
    }

    private IEnumerator HandleChapterEventsCo()
    {
        while (currentEvent != null)
        {
            currentTransitionData = currentEvent.TransitionData;

            if (currentTransitionData.Sprite != null)
                yield return StartCoroutine(FadeNewSpriteInCo());

            if (currentTransitionData.Texts != null || currentTransitionData.Texts.Count > 1)
                yield return StartCoroutine(ShowTextCo());

            // TODO: show indicator

            while (!Input.GetKeyDown(KeyCode.Space)) 
                yield return null;

            string onEndEventKey;

            if (currentEvent is ChapterQuestionEvent questionEvent)
            {
                questionDisplayer.gameObject.SetActive(true);
                questionDisplayer.Display(questionEvent.Alternatives);

                while (!playerChoseAlternativeThisFrame)
                    yield return null;

                playerChoseAlternativeThisFrame = false;

                onEndEventKey = questionEvent.Alternatives[alternativeChosenNumber].onChoose.Key;
            }
            else
            {
                onEndEventKey = currentEvent.OnEndEvent.Key;
            }

            currentEvent = currentChapter.GetChapterEvent(onEndEventKey);
        }

        // TODO: Implement finishing chapter
    }

    private IEnumerator FadeNewSpriteInCo()
    {
        yield return StartCoroutine(imageFader.FadeNewImageCo(currentTransitionData.Sprite, currentTransitionData.FadeInTime));
    }

    private IEnumerator ShowTextCo()
    {
        // TODO: Player skip text

        foreach (var text in currentTransitionData.Texts)
        {
            yield return StartCoroutine(textAnimator.AnimateTextCo(text, .06f));

            while (!Input.GetKeyDown(KeyCode.Space))
                yield return null;
        }
    }
}
