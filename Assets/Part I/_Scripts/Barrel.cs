using UnityEngine;

[ExecuteAlways]
public sealed class Barrel : MonoBehaviour {
    private static readonly int prop_color_id = Shader.PropertyToID( "_Color" );

    [SerializeField] private BarrelDataSO barrelDataSo;

    private MaterialPropertyBlock mpb; // Gonna be null on each assembly reload
    public MaterialPropertyBlock Mpb {
        get
        {
            if ( mpb == null )
                mpb = new MaterialPropertyBlock();
            return mpb;
        }
    }

    /// <summary> Called everytime you modify a value in the inspector </summary>
    private void OnValidate() => TryApplyColor();

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

    // This gonna be in the context menu of this component
    [ContextMenu( "Try Apply Color" )]
    public void TryApplyColor() {
        if ( barrelDataSo == null )
            return;

        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        Mpb.SetColor( prop_color_id, barrelDataSo.Color );
        meshRenderer.SetPropertyBlock( Mpb );
    }

    private void OnDrawGizmos() {
        if ( barrelDataSo == null )
            return;

        Gizmos.DrawWireSphere( transform.position, barrelDataSo.Radius );
    }
}