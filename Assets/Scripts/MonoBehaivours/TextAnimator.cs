using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextAnimator : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI textComp;
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private SoundEffect letterSfx;

    public IEnumerator AnimateTextCo(string newText, float charIntervalInSec)
    {
        var waitInterval = new WaitForSeconds(charIntervalInSec);

        textComp.text = string.Empty;

        foreach (var c in newText)
        {
            if (char.IsLetterOrDigit(c))
                soundManager.PlaySoundEffect(letterSfx);

            textComp.text += c;
            yield return waitInterval;
        }
        
    }
}
