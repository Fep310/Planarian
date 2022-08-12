using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New ChapterTrigger Asset", menuName = "Story/Chapter/New Trigger")]
public class ChapterTrigger : ScriptableObject
{
    [SerializeField] private string key;
    public string Key { get => key; }
}
