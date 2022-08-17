using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : InitializableMonoBehaviour
{
    [SerializeField] private ScreenFader fader;
    [SerializeField] private List<AlternativeButton> mainButtons;

    public override void Init()
    {
        StartCoroutine(fader.FadeCo(true, 2));
    }

    public void OnPlay()
    {
    }

    public void OnOptions()
    {
    }

    public void OnQuit()
    {
        mainButtons.ForEach(b => b.SetInteractable(false));
        StartCoroutine(QuitCo());
        IEnumerator QuitCo()
        {
            yield return StartCoroutine(fader.FadeCo(false, 1.5f));
            Application.Quit();
        }
    }
}
