using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryPlayer : InitializableMonoBehaviour
{
    [SerializeField] private Story story;
    [Space]
    [SerializeField] private VisualDisplayer visualDisplayer;
    [SerializeField] private TextAnimator textAnimator;
    [SerializeField] private QuestionDisplayer questionDisplayer;
    [SerializeField] private ScreenFader screenFader;
    [SerializeField] private DialogueArrow dialogueArrow;
    [Space]
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private SoundEffect nextTextSfx;

    [HideInInspector] public bool playerChoseAlternativeThisFrame;
    [HideInInspector] public int alternativeChosenNumber;

    private Chapter currentChapter;
    private ChapterEvent currentEvent;
    private EventTransitionData currentEventData;

    private const float FADE_IN_DURATION = 3;
    private const float FADE_OUT_DURATION = 3;
    private const float DEFAULT_CHAR_INTERVAL = .1f;

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
        currentChapter = story.NewGame();
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

        yield return StartCoroutine(screenFader.FadeCo(true, FADE_IN_DURATION));

        while (true)
        {
            currentEventData = currentEvent.TransitionData;

            if (currentEventData.Sound != null)
                soundManager.PlaySoundEffect(currentEventData.Sound);

            if (currentEventData.Track != null)
            {
                if (currentEventData.ShouldEnqueueTrack)
                    soundManager.EnqueueSoundtrack(currentEventData.Track, currentEventData.ShouldTrackLoop);
                else
                    soundManager.PlaySoundtrack(currentEventData.Track, currentEventData.ShouldTrackLoop);
            }
            else
            {
                if (currentEventData.StopSoundtrack)
                {
                    soundManager.StopSoundtrack(currentEventData.TrackFadeOutDuration);
                }
            }

            if (currentEventData.Sprite != null)
                yield return StartCoroutine(FadeNewSpriteInCo());

            if (currentEventData.AnimSprites != null && currentEventData.AnimSprites.Length > 0)
                yield return StartCoroutine(FadeNewAnimInCo());

            if (currentEventData.ImageWaitTime > 0)
                yield return new WaitForSeconds(currentEventData.ImageWaitTime);

            if (currentEventData.Texts != null || currentEventData.Texts.Count > 1)
                yield return StartCoroutine(ShowTextCo());

            textAnimator.ResetText();

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
                if (currentEvent.OnEndEvent == null)
                    break;

                onEndEventKey = currentEvent.OnEndEvent.Key;
            }

            currentEvent = currentChapter.GetChapterEvent(onEndEventKey);
        }

        soundManager.StopSoundtrack(FADE_OUT_DURATION);
        yield return StartCoroutine(screenFader.FadeCo(false, FADE_OUT_DURATION));
        // TODO: Implement finishing chapter
    }

    private IEnumerator FadeNewSpriteInCo()
    {
        yield return StartCoroutine(visualDisplayer.FadeNewImageCo(currentEventData.Sprite, currentEventData.FadeInTime));
    }

    private IEnumerator FadeNewAnimInCo()
    {
        yield return StartCoroutine(visualDisplayer.FadeNewAnimationCo(currentEventData.AnimSprites, currentEventData.FadeInTime, currentEventData.AnimFps));
    }

    private IEnumerator ShowTextCo()
    {
        foreach (var text in currentEventData.Texts)
        {
            yield return new WaitForSeconds(.33f);

            textAnimator.AnimateText(text, DEFAULT_CHAR_INTERVAL);
            
            while (textAnimator.IsAnimating)
                yield return null;

            yield return null; // If we don't wait for another frame, the CanContinueEvent() returns true the same frame!

            dialogueArrow.Toggle(true);

            while (!CanContinueEvent())
                yield return null;

            dialogueArrow.Toggle(false);
            soundManager.PlaySoundEffect(nextTextSfx);
        }
    }
}
