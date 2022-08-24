using UnityEngine;

[System.Serializable]
public struct StoryCondition
{
    [SerializeField] private string key;
    public string Key { get => key; }

    [SerializeField] private StoryValue valueRef;
    public StoryValue ValueRef { get => valueRef; }

    [SerializeField] private ConditionType conditionOperator;
    public ConditionType ConditionOperator { get => conditionOperator; }

    [SerializeField] private int neededValue;
    public int NeededValue { get => neededValue; }

    public bool IsConditionMet()
    {

        return conditionOperator switch
        {
            ConditionType.EQUAL => valueRef.Value == neededValue,
            ConditionType.NOT_EQUAL => valueRef.Value != neededValue,
            ConditionType.GREATER_THAN => valueRef.Value > neededValue,
            ConditionType.LESS_THAN => valueRef.Value < neededValue,
            _ => false,
        };
    }
}
