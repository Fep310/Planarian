using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextAnimator : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI textComp;

    public IEnumerator AnimateTextCo(string newText, float charIntervalInSec)
    {
        var waitInterval = new WaitForSeconds(charIntervalInSec);

        foreach (var c in newText)
        {
            textComp.text += c;
            yield return waitInterval;
        }
        
    }
}
