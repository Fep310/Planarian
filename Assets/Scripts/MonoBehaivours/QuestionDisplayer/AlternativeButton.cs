using System;
using UnityEngine;
using UnityEngine.UI;

public class AlternativeButton : Selectable
{
    /*
     * SetState(true):
     *  OnMouseEnter()
     *  OnSelect()
     * 
     * SetState(false):
     *  OnMouseExit()
     *  OnDeselect
     * 
     * Choose():
     *  OnSubmit()
     *  OnMouseClick()
     */

    // TODO: Can't navigate with keyboard

    [SerializeField] private Image background;
    [SerializeField] private TMPro.TextMeshProUGUI textMeshPro;
    [SerializeField] private int alternativeNumber;
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private SoundEffect hoverAlternativeSfx;

    public Action<int> onChoose;

    public void SetText(string newText)
    {
        textMeshPro.text = newText;
    }

    public void SetState(bool b)
    {
        background.color = b ? Color.white : Color.black;
        textMeshPro.color = b ? Color.black : Color.white;
        textMeshPro.fontStyle = b ? TMPro.FontStyles.Bold : TMPro.FontStyles.Normal;

        // TODO: Pitch based on how impactful is alternative
        if (b)
            soundManager.PlaySoundEffectRandPitch(hoverAlternativeSfx, hoverAlternativeSfx.DefaultVolume, .8f, 1.2f);
    }

    public void Choose()
    {
        onChoose?.Invoke(alternativeNumber);
    }
}
