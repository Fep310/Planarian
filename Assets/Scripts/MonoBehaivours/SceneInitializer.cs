using System.Collections.Generic;
using UnityEngine;

public class SceneInitializer : MonoBehaviour
{
    [SerializeField] private List<InitializableMonoBehaviour> initializablesInOrder;

    private void Awake() => initializablesInOrder.ForEach(i => i.Init());
}
