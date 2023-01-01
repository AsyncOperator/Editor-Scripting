using UnityEngine;
using UnityEditor;
using Extensions;

public class SnapperTool : EditorWindow {
    private const string UNDO_SELECTED_SNAP_OBJECTS = "Snapped Objects";

    private Grid grid;
    private float size;

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

        if ( grid == Grid.Polar ) {
            GUILayout.Label( "Polar" );
        }

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
            Vector3 startPosition = new( 10f, 0f, 10f );
            int lineCount = 16;

            for ( int i = 0 ; i <= lineCount ; i++ ) {
                Vector3 zModified = startPosition.Z( startPosition.z - i * size );
                Vector3 xModified = startPosition.X( startPosition.x - i * size );

                Handles.DrawAAPolyLine( zModified, zModified.X( -size * lineCount + startPosition.x ) );
                Handles.DrawAAPolyLine( xModified, xModified.Z( -size * lineCount + startPosition.z ) );
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