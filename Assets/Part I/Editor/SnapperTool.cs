using Extensions;
using UnityEditor;
using UnityEngine;

public class SnapperTool : EditorWindow {
    private const string UNDO_SELECTED_SNAP_OBJECTS = "Snapped Objects";

    private Grid grid;
    private float size;

    private Rect rect;

    [MenuItem( "Tools/Snapper" )]
    public static void OpenSnapperWindow() => GetWindow<SnapperTool>( title: "Snapper" ); // Support single window, if no window exist create and focus, otherwise just focus existing

    private void OnEnable() {
        Selection.selectionChanged += Repaint;
        SceneView.duringSceneGui += Draw;
    }

    private void OnDisable() {
        Selection.selectionChanged -= Repaint;
        SceneView.duringSceneGui -= Draw;
    }

    private void OnGUI() {
        grid = (Grid)EditorGUILayout.EnumPopup( "Grid Type: ", grid );
        size = EditorGUILayout.FloatField( "Size: ", size );
        SceneView.RepaintAll();

        HandleGridType();

        using ( new EditorGUI.DisabledScope( ValidateCondition() ) ) {
            if ( GUILayout.Button( "Snap Selection" ) ) {
                SnapSelection();
            }
        }
    }

    private void HandleGridType() {
        if ( grid == Grid.Cartesian ) {
            rect = EditorGUILayout.RectField( "Boundaries: ", rect );
        }
        else if ( grid == Grid.Polar ) {
            GUILayout.Label( "Polar" );
        }
    }

    private bool ValidateCondition() => Selection.gameObjects.Length == 0;

    private void SnapSelection() {
        foreach ( GameObject gameObject in Selection.gameObjects ) {
            Undo.RecordObject( gameObject.transform, UNDO_SELECTED_SNAP_OBJECTS );
            gameObject.transform.position = gameObject.transform.position.Round();
        }
    }

    private void Draw( SceneView sceneView ) {
        switch ( grid ) {
            case Grid.Cartesian:
                DrawCartesianGrid();
                break;
            case Grid.Polar:
                DrawPolarGrid();
                break;
        }

        void DrawCartesianGrid() {
            Vector3 startPosition = new( rect.x, 0f, rect.y ); // Top-left
            Vector3 endPosition = startPosition + new Vector3( rect.width, 0f, -rect.height ); // Bottom-right

            for ( float x = 0 ; x <= Mathf.Abs( endPosition.x - startPosition.x ) ; x += size ) {
                Vector3 pointA = startPosition + new Vector3( x, 0f, 0f );
                Vector3 pointB = new Vector3( pointA.x, pointA.y, endPosition.z );

                Handles.DrawAAPolyLine( pointA, pointB );
            }

            for ( float z = 0 ; z <= Mathf.Abs( endPosition.z - startPosition.z ) ; z += size ) {
                Vector3 pointA = startPosition + new Vector3( 0f, 0f, -z );
                Vector3 pointB = new Vector3( endPosition.x, pointA.y, pointA.z );

                Handles.DrawAAPolyLine( pointA, pointB );
            }
        }

        void DrawPolarGrid() {

        }
    }

    private enum Grid {
        Cartesian,
        Polar
    }
}