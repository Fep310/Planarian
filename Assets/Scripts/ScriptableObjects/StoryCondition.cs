using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New StoryCondition Asset", menuName = "Planarian Assets/Condition")]
public class StoryCondition : ScriptableObject
{
    [SerializeField] private string key;
    public string Key { get => key; }

    [SerializeField] private StoryValue valueRef;
    public StoryValue ValueRef { get => valueRef; }

    [SerializeField] private ConditionType condition;
    public ConditionType Condition { get => condition; }

    [SerializeField] private int neededValue;
    public int NeededValue { get => neededValue; }
}
