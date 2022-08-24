using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextAnimator : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI textComp;
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private SoundEffect letterSfx;

    private bool isAnimating;
    public bool IsAnimating => isAnimating;

    private string newText;
    private Coroutine animatingCoroutine;
    private Color transparent = new Color(1, 1, 1, 0);
    private WaitForSeconds waitInterval;
    private WaitForSeconds colenWaitInterval;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            Skip();
    }

    public void Init()
    {
        textComp.text = string.Empty;
    }

    public void AnimateText(string newText, float charIntervalInSec)
    {
        this.newText = newText;
        isAnimating = true;

        if (waitInterval == null || colenWaitInterval == null)
        {
            waitInterval = new WaitForSeconds(charIntervalInSec);
            colenWaitInterval = new WaitForSeconds(charIntervalInSec * 2);
        }

        animatingCoroutine = StartCoroutine(AnimateTextCo(charIntervalInSec));
    }

    public IEnumerator AnimateTextCo(float charIntervalInSec)
    {
        bool insideColon = false;

        textComp.text = string.Empty;

        foreach (var c in newText)
        {
            if (insideColon)
            {
                textComp.text += c;

                if (c == ':')
                {
                    insideColon = false;
                    yield return colenWaitInterval;
                }

                continue;
            }

            if (c == ':')
            {
                insideColon = true;
                continue;
            }

            yield return waitInterval;

            if (char.IsLetterOrDigit(c))
                soundManager.PlaySoundEffectRandPitch(letterSfx, letterSfx.DefaultVolume, .9f, 1.1f);

            textComp.text += c;
        }

        isAnimating = false;
    }

    private void Skip()
    {
        if (!isAnimating)
            return;

        StopCoroutine(animatingCoroutine);
        textComp.text = newText;
        isAnimating = false;
    }
}
