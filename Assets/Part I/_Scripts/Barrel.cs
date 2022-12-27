using UnityEngine;

[ExecuteAlways]
public sealed class Barrel : MonoBehaviour {
    [Range( 1f, 10f ), SerializeField] private float radius;
    [SerializeField] private float damage;
    [SerializeField] private Color color;


    private void OnEnable() {
        BarrelManager.AddToList( this );
    }

    private void OnDisable() {
        BarrelManager.RemoveFromList( this );
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere( transform.position, radius );
    }
}