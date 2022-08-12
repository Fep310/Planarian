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
    }

    public void Choose()
    {
        onChoose?.Invoke(alternativeNumber);
    }
}
