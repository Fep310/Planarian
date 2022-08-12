using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Chapter Asset", menuName = "Story/New Condition")]
public class StoryCondition : ScriptableObject
{
    [SerializeField] private string key;
    public string Key { get => key; }

    [SerializeField] private StoryValue valueRef;
    public StoryValue ValueRef { get => valueRef; }

    [SerializeField] private ConditionType conditionOperator;
    public ConditionType ConditionOperator { get => conditionOperator; }

    [SerializeField] private int neededValue;
    public int NeededValue { get => neededValue; }

    public bool IsConditionMet() {

        switch (conditionOperator) {

            case ConditionType.EQUAL:
                return valueRef.Value == neededValue;

            case ConditionType.NOT_EQUAL:
                return valueRef.Value != neededValue;

            case ConditionType.GREATER_THAN:
                return valueRef.Value > neededValue;

            case ConditionType.LESS_THAN:
                return valueRef.Value < neededValue;
        }

        return false;
    }
}
