using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        mainButtons.ForEach(b => b.SetInteractable(false));
        StartCoroutine(PlayCo());
        IEnumerator PlayCo()
        {
            yield return StartCoroutine(fader.FadeCo(false, 1.5f));
            yield return SceneManager.LoadSceneAsync(1);
        }
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
