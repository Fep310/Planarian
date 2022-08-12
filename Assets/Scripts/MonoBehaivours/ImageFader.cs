using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageFader : MonoBehaviour
{
    [SerializeField] private Image backImage;
    [SerializeField] private Image frontImage;

    private Color transparent = new Color(1, 1, 1, 0);

    public IEnumerator FadeNewImageCo(Sprite newSprite, float fadeTime)
    {
        backImage.sprite = frontImage.sprite;
        frontImage.sprite = newSprite;
        frontImage.color = transparent;

        float startTime = Time.time;
        float endTime = startTime + fadeTime;
        float t;

        while (Time.time < endTime)
        {
            t = Mathf.InverseLerp(startTime, endTime, Time.time);
            frontImage.color = new Color(1, 1, 1, t);
            yield return null;
        }

        frontImage.color = Color.white;
    }
}
