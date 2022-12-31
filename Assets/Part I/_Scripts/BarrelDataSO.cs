using UnityEngine;

[CreateAssetMenu]
public sealed class BarrelDataSO : ScriptableObject {
    [field: SerializeField] public float Radius { get; private set; }
    [field: SerializeField] public float Damage { get; private set; }
    [field: SerializeField] public Color Color { get; private set; }
}