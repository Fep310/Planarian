using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct QuestionAlternative
{
    public string label;
    public ChapterTrigger onChoose;
}
