using FullInspector;
using UnityEditor;
using UnityEngine;

[CustomBehaviorEditor(typeof(Position))]
public class PositionEditor : BehaviorEditor<Position>
{
    protected override void OnEdit(Rect rect, Position pos, fiGraphMetadata metadata)
    {
        DefaultBehaviorEditor.Edit(rect, pos, metadata);
        pos.transform.position = new Vector3(pos.X, pos.Y);
    }

    protected override float OnGetHeight(Position behavior, fiGraphMetadata metadata)
    {
        return DefaultBehaviorEditor.GetHeight(behavior, metadata);
    }

    protected override void OnSceneGUI(Position pos)
    {
    }
}