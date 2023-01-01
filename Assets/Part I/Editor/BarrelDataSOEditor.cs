using UnityEditor;
using UnityEngine;

[CustomEditor( typeof( BarrelDataSO ) )]
public sealed class BarrelDataSOEditor : Editor {
    private SerializedObject so;

    SerializedProperty propertyRadius;
    SerializedProperty propertyDamage;
    SerializedProperty propertyColor;

    private void OnEnable() {
        so = serializedObject; // Or so = new( target );

        propertyRadius = so.FindProperty( "Radius" );
        propertyDamage = so.FindProperty( "Damage" );
        propertyColor = so.FindProperty( "Color" );
    }

    public override void OnInspectorGUI() {
        // GUI             => Contains both in game and in editor UI functions, explicit positioning using Rect
        // GUILayout       => Contains both in game and in editor UI functions, implicit positioning, auto-layout
        // EditorGUI       => Editor UI functions, explicit positioning using Rect
        // EditorGUILayout => Editor UI functions, implicit positioning, auto-layout

        BetterWay();
    }

    // Do not support multi-selecting, should add [CanEditMultipleObjects] attribute top of the class
    // Should add boiler-plate code for every field we want to change to support undo system
    private void ManualAndBadWay() {
        BarrelDataSO barrelDataTarget = target as BarrelDataSO;

        float newRadius = EditorGUILayout.FloatField( "Radius : ", barrelDataTarget.Radius );
        if ( newRadius != barrelDataTarget.Radius ) {
            Undo.RecordObject( barrelDataTarget, "Change barrel radius" );
            barrelDataTarget.Radius = newRadius;
        }
    }

    // Recognize scene changes and mark as dirty,
    // Can support multi-selecting
    // Automatically support undo system
    private void BetterWay() {
        // Before anything else that can modify property values
        so.Update(); // Preparing the object for change
        EditorGUILayout.PropertyField( propertyRadius );
        EditorGUILayout.PropertyField( propertyDamage );
        EditorGUILayout.PropertyField( propertyColor );
        if ( so.ApplyModifiedProperties() ) {
            // If something changes enter this block
            BarrelManager.UpdateAllBarrelsColor();
        }
    }
}