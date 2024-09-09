using TriggerSystem.Data;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameEventData))]
public class GameEventDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var data = target as GameEventData;

        EditorGUILayout.LabelField($"Triggers count: {data.GetTriggersCount()}");

        if (GUILayout.Button("Clear Triggers"))
        {
            data.ClearTriggers();
        }
    }
}
