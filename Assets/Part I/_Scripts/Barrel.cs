using UnityEngine;

[ExecuteAlways]
public sealed class Barrel : MonoBehaviour {
    [Range( 1f, 10f ), SerializeField] private float radius;
    [SerializeField] private float damage;
    [SerializeField] private Color color;

    private void Awake() {
        /*
         * GetComponent<Renderer>().material.color = color; // Bad example; this line of code actually instantiate the duplicate of the material that is assigned on the renderer component
         * GetComponent<Renderer>().sharedMaterial.color = color; // Bad example; but still a better approach from the previous 'loc', this will modify the actual material asset
         * 
         * If you find yourself in a situation where you do need to make a material or create any kind of assets,
         * but you want to make sure that you're not leaking materials/meshes into the scene
         * 
         * Shader shader = Shader.Find( "Default/Diffuse" );
         * Material material = new ( shader ); // This is an asset and this is gonna stick around and live forever until you tell it to destroy itself
         * material.hideFlags = HideFlags.HideAndDontSave; // Now, this asset is not going to be saved so if you leave the scene it's gonna unload this asset, preventing memory leaks in the editor
         */
    }

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