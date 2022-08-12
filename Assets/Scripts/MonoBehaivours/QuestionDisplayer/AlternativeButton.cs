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

    [SerializeField] private Image background;
    [SerializeField] private TMPro.TextMeshProUGUI text;
    [SerializeField] private int alternativeNumber;

    public Action<int> onChoose;

    public void SetState(bool b)
    {
        background.color = b ? Color.white : Color.black;
        text.color = b ? Color.black : Color.white;
        text.fontStyle = b ? TMPro.FontStyles.Bold : TMPro.FontStyles.Normal;
    }

    public void Choose()
    {
        onChoose?.Invoke(alternativeNumber);
    }
}
