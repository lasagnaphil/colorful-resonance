using Rotorz.ReorderableList;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MapLoader))]
public class MapLoaderEditor : Editor
{
    private SerializedProperty mapAssetListProperty;
    private SerializedProperty mapToLoadProperty;

    void OnEnable()
    {
        mapAssetListProperty = serializedObject.FindProperty("mapAssetList");
        mapToLoadProperty = serializedObject.FindProperty("mapToLoad");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(mapToLoadProperty);
        EditorGUILayout.LabelField("Map Asset List:");
        ReorderableListGUI.ListField(mapAssetListProperty);
        serializedObject.ApplyModifiedProperties();
    }
}
