using UnityEngine;
using UnityEditor;

public static class Snapper {
    private const string UNDO_SELECTED_SNAP_OBJECTS = "Snapped Objects";

    [MenuItem( "Edit/Snap Selected Objects %&G" )] // Shortcut keys for this menu item ( % => CTRL, # => SHIFT, & => ALT )
    public static void SnapGameObjects() {
        // Get the selected gameobject in the hiearchy
        // Record before moving them not after, otherwise it's not gonna work!
        foreach ( GameObject gameObject in Selection.gameObjects ) {
            Undo.RecordObject( gameObject.transform, UNDO_SELECTED_SNAP_OBJECTS ); // Be specific, remember that this one takes a single unity object
            gameObject.transform.position = gameObject.transform.position.Round();
        }
    }

    [MenuItem( "Edit/Snap Selected Objects %&G", isValidateFunction: true )]
    public static bool SnapGameObjectsValidate() => Selection.gameObjects.Length > 0; // Greyed out when no objects selected so, so nothing can be snapped

    public static Vector3 Round( this Vector3 v ) => new( Mathf.Round( v.x ), Mathf.Round( v.y ), Mathf.Round( v.z ) );
}