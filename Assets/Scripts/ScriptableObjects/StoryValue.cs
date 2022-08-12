using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New StoryValue Asset", menuName = "Story/New Value")]
public class StoryValue : ScriptableObject
{
    [SerializeField] private string key;
    public string Key { get => key; }

    [SerializeField] private int value;
    public int Value {
        get => value;
        set => this.value = value;
    }

    [SerializeField] private int defaultValue;

    public void ResetValue() => value = defaultValue;
}
