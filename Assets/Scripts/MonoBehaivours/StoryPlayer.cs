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
    [SerializeField] private ScreenFader screenFader;
    [SerializeField] private DialogueArrow dialogueArrow;

    [HideInInspector] public bool playerChoseAlternativeThisFrame;
    [HideInInspector] public int alternativeChosenNumber;

    private Chapter currentChapter;
    private ChapterEvent currentEvent;
    private EventTransitionData currentTransitionData;

    private const float FADE_OUT_DURATION = 1;
    private const float DEFAULT_CHAR_INTERVAL = .04f;

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
        textAnimator.ResetText();
        screenFader.Init();

        StartCoroutine(HandleChapterEventsCo());
    }

    private bool CanContinueEvent() => Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0);

    private IEnumerator HandleChapterEventsCo()
    {
        // TODO: Chapter introduction

        yield return new WaitForSeconds(.5f);

        yield return StartCoroutine(screenFader.FadeCo(true, FADE_OUT_DURATION));

        while (currentEvent != null)
        {
            currentTransitionData = currentEvent.TransitionData;

            if (currentTransitionData.Sprite != null)
            {
                yield return StartCoroutine(FadeNewSpriteInCo());
            }

            if (currentTransitionData.Texts != null || currentTransitionData.Texts.Count > 1)
            {
                yield return StartCoroutine(ShowTextCo());
            }

            dialogueArrow.Toggle(true);

            while (!CanContinueEvent())
                yield return null;

            dialogueArrow.Toggle(false);

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

        yield return StartCoroutine(screenFader.FadeCo(false, FADE_OUT_DURATION));

        // TODO: Implement finishing chapter
    }

    private IEnumerator FadeNewSpriteInCo()
    {
        yield return StartCoroutine(imageFader.FadeNewImageCo(currentTransitionData.Sprite, currentTransitionData.FadeInTime));
    }

    private IEnumerator ShowTextCo()
    {
        foreach (var text in currentTransitionData.Texts)
        {
            textAnimator.AnimateText(text, DEFAULT_CHAR_INTERVAL);
            
            while (textAnimator.IsAnimating)
                yield return null;

            yield return null; // If we don't wait for another frame, the CanContinueEvent() returns true the same frame!
        }
    }
}
