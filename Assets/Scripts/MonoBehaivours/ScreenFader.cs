using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour
{
    [SerializeField] private Image imageFade;

    public IEnumerator FadeCo(bool isFadingIn, float fadeTime)
    {
        float startTime = Time.time;
        float endTime = startTime + fadeTime;
        float t;

        while (Time.time < endTime)
        {
            t = Mathf.InverseLerp(startTime, endTime, Time.time);
            t = isFadingIn ? -t + 1 : t;
            imageFade.color = new Color(0, 0, 0, t);
            yield return null;
        }

        imageFade.color = new Color(0, 0, 0, isFadingIn ? 0 : 1);
    }

    public void Init()
    {
        imageFade.color = Color.black;
    }
}
