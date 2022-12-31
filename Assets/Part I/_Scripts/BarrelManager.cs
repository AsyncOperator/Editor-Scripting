using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public sealed class BarrelManager : MonoBehaviour {
    private static List<Barrel> barrels = new();

    public static void AddToList( Barrel barrel ) => barrels.Add( barrel );

    public static void RemoveFromList( Barrel barrel ) => barrels.Remove( barrel );

    private void OnDrawGizmos() {
        if ( barrels.Count > 0 ) {
            foreach ( Barrel barrel in barrels ) {
                //Gizmos.DrawLine( transform.position, barrel.transform.position );

                // Drawing anti-aliasing line nothing to fancy, some sort of equivalent of Gizmos.DrawLine
#if UNITY_EDITOR
                Handles.DrawAAPolyLine( transform.position, barrel.transform.position );
#endif
            }
        }
    }
}