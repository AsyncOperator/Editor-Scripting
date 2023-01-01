using UnityEngine;
using UnityEditor;

public class SnapperTool : EditorWindow {
    private const string UNDO_SELECTED_SNAP_OBJECTS = "Snapped Objects";

    [MenuItem( "Tools/Snapper" )]
    public static void OpenSnapperWindow() => GetWindow<SnapperTool>( title: "Snapper" ); // Support single window, if no window exist create and focus, otherwise just focus existing

    private void OnEnable() {
        Selection.selectionChanged += Repaint;
    }

    private void OnDisable() {
        Selection.selectionChanged -= Repaint;
    }

    private void OnGUI() {
        using ( new EditorGUI.DisabledScope( ValidateCondition() ) ) {
            if ( GUILayout.Button( "Snap Selection" ) ) {
                SnapSelection();
            }
        }
    }

    private bool ValidateCondition() => Selection.gameObjects.Length == 0;

    private void SnapSelection() {
        foreach ( GameObject gameObject in Selection.gameObjects ) {
            Undo.RecordObject( gameObject.transform, UNDO_SELECTED_SNAP_OBJECTS );
            gameObject.transform.position = gameObject.transform.position.Round();
        }
    }
}