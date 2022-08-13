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

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            Skip();
    }

    public void ResetText()
    {
        textComp.text = string.Empty;
    }

    public void AnimateText(string newText, float charIntervalInSec)
    {
        this.newText = newText;
        isAnimating = true;
        animatingCoroutine = StartCoroutine(AnimateTextCo(charIntervalInSec));
    }

    public IEnumerator AnimateTextCo(float charIntervalInSec)
    {
        var waitInterval = new WaitForSeconds(charIntervalInSec);

        textComp.text = string.Empty;

        foreach (var c in newText)
        {
            yield return waitInterval;

            if (char.IsLetterOrDigit(c))
                soundManager.PlaySoundEffect(letterSfx);

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
