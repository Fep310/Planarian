using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisualDisplayer : MonoBehaviour
{
    [SerializeField] private Image backImage;
    [SerializeField] private Image frontImage;
    [SerializeField] private Image fullScreenImage;

    private Image animatingImage;
    private Sprite[] animationSprites;
    private Color transparent = new(1, 1, 1, 0);
    private Coroutine animationCoroutine;
    private bool fullscreen;

    public void Jumpscare(Sprite sprite)
    {
        fullscreen = true;

        fullScreenImage.sprite = sprite;
        fullScreenImage.color = Color.white;

        backImage.color = transparent;
        frontImage.color = transparent;
    }

    public IEnumerator FadeNewImageCo(Sprite newSprite, float fadeTime)
    {
        if (fullscreen)
        {
            fullScreenImage.color = transparent;
            fullscreen = false;
        }

        if (animationCoroutine != null)
            animatingImage = backImage;

        backImage.sprite = frontImage.sprite;
        backImage.color = Color.white;
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

        if (animationCoroutine != null)
            StopCoroutine(animationCoroutine);
    }

    public IEnumerator FadeNewAnimationCo(Sprite[] newAnim, float fadeTime, int animFps)
    {
        if (fullscreen)
        {
            fullScreenImage.color = transparent;
            fullscreen = false;
        }

        animationSprites = newAnim;

        backImage.sprite = frontImage.sprite;
        backImage.color = Color.white;
        frontImage.sprite = animationSprites[0];
        frontImage.color = transparent;

        animatingImage = frontImage;
        animationCoroutine = StartCoroutine(AnimationCo(animFps));

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

    private IEnumerator AnimationCo(int fps)
    {
        int i = 0;
        var wait = new WaitForSeconds(1 / (float)fps);

        while (true)
        {
            animatingImage.sprite = animationSprites[i];
            
            i++;
            if (i > animationSprites.Length)
                i = 0;

            yield return wait;
        }
    }
}
