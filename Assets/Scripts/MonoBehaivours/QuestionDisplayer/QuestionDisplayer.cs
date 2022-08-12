using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionDisplayer : MonoBehaviour
{
    public List<AlternativeButton> alternativeButtons;

    private void OnEnable()
    {
        foreach (var btn in alternativeButtons)
            btn.onChoose += (_) => Close();
    }

    private void OnDisable()
    {
        foreach (var btn in alternativeButtons)
            btn.onChoose -= (_) => Close();
    }

    public void Display(List<QuestionAlternative> alternatives)
    {
        for (int i = 0; i < alternatives.Count; i++)
            alternativeButtons[i].gameObject.SetActive(true);
    }

    public void Close()
    {
        alternativeButtons.ForEach(b => b.gameObject.SetActive(false));
        gameObject.SetActive(false);
    }
}
