using UnityEngine;

[CreateAssetMenu(fileName = "TerrainType", menuName = "Game/TerrainType")]
public class TerrainType : ScriptableObject
{
    [SerializeField] string terrainName;
    [SerializeField] Color gizmoColor;
    [SerializeField] bool walkable;
    [SerializeField] int mvmntCost;
    [SerializeField] Texture2D terrainTexture;

    public string TerrainName => terrainName;
    public Color GizmoColor => gizmoColor;
    public bool Walkable => walkable;
    public int MvmntCost => mvmntCost;
    public Texture2D TerrainTexture => terrainTexture;
}
