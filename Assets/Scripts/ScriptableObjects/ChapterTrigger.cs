using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Trigger", menuName = "Story/Chapter/New Trigger")]
public class ChapterTrigger : ScriptableObject
{
    [SerializeField] private string key;
    public string Key { get => key; }
}
