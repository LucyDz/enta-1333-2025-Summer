using UnityEngine;

[System.Serializable]
public struct GridNode
{
    public string Name;
    public Vector3 WorldPosition;
    public TerrainType TerrainType;

    public bool Walkable => TerrainType?.Walkable ?? false;
    public int Weight => TerrainType?.MvmntCost ?? 1;
    public Color GizmoColor => TerrainType?.GizmoColor ?? Color.gray;
}
