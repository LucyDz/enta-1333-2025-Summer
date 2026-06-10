using UnityEngine;

[CreateAssetMenu(fileName = "GridSettings", menuName = "Game/GridSettings")]
public class GridSettings : ScriptableObject
{
    [SerializeField] private Vector2 _gridSize = new(1, 1);
    [SerializeField] private float _nodeSize = 1;
    [SerializeField] private bool _useXZPlane;
    [SerializeField] private bool _allowDiagonal = false;

    public Vector2 GridSize => _gridSize;
    public float NodeSize => _nodeSize;
    public bool UseXZPlane => _useXZPlane;
    public bool AllowDiagonal => _allowDiagonal;

}
