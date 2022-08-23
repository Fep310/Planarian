using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct QuestionAlternative
{
    public string Label;
    public ChapterTrigger OnChoose;
    public bool ChangeStoryValue;

    [SerializeField] private StoryValue storyValue;
    [SerializeField] private int valueTo;
    [SerializeField] private OperationType operation;

    public void SetNewValue()
    {
        switch (operation)
        {
            case OperationType.SET:
                storyValue.Value = valueTo;
                break;
            case OperationType.ADD:
                storyValue.Value += valueTo;
                break;
            case OperationType.SUBTRACT:
                storyValue.Value -= valueTo;
                break;
            case OperationType.MULTIPLY:
                storyValue.Value *= valueTo;
                break;
            case OperationType.DIVIDE:
                storyValue.Value /= valueTo;
                break;
        }
    }
}
