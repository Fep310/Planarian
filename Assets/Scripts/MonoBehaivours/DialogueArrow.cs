using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueArrow : MonoBehaviour
{
    [SerializeField] private Image image;

    private Coroutine fadeCoroutine;
    private Color transparent = new Color(1, 1, 1, 0);

    public void Toggle(bool b)
    {
        if (b)
        {
            fadeCoroutine = StartCoroutine(FadeCo());
            return;
        }

        StopCoroutine(fadeCoroutine);
        image.color = transparent;
    }

    private const float FADE_IN_DURATION = .3f;

    private IEnumerator FadeCo()
    {
        float startTime = Time.time;
        float endTime = startTime + FADE_IN_DURATION;
        float t;

        while (Time.time < endTime)
        {
            t = Mathf.InverseLerp(startTime, endTime, Time.time);
            image.color = new Color(1, 1, 1, t);
            yield return null;
        }

        image.color = Color.white;
    }
}
