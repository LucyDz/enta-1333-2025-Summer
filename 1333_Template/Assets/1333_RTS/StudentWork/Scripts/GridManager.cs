using NUnit.Framework;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] GridSettings _gridSettings;

    public GridSettings GridSettings => _gridSettings;
    public bool IsInitialized { get; private set; } = false;

    private GridNode[,] gridNodes;

#if UNITY_EDITOR
    [Header("Debug")]
    [SerializeField] private List<GridNode> DebugNodes = new();
#endif

    public void InitGrid()
    {
        if (IsInitialized)
            return;

        gridNodes = new GridNode[(int)_gridSettings.GridSize.x, (int)_gridSettings.GridSize.y];

        for (int x = 0; x < _gridSettings.GridSize.x; x++) {
            for (int y = 0; y < _gridSettings.GridSize.y; y++)
            {
                Vector3 worldPos = _gridSettings.UseXZPlane ? new(x, 0, y) : new(x, y, 0);

                GridNode node = new GridNode
                {
                    Name = $"Cell_[{x},{y}]",
                    WorldPosition = worldPos * _gridSettings.NodeSize,
                    //Walkable = true,
                    //Weight = 1,
                };

                gridNodes[x, y] = node;
            } 
        }

        IsInitialized = true;
    }

#if UNITY_EDITOR
    private void PopulateDebugList()
    {
        DebugNodes.Clear();

        for (int x = 0; x < gridNodes.GetLength(0); x++)
        {
            for (int y = 0; y < gridNodes.GetLength(1); y++)
            {
                GridNode node = gridNodes[x, y];
                DebugNodes.Add(new GridNode
                {
                    Name = node.Name,
                    WorldPosition = node.WorldPosition,
                    //Walkable = node.Walkable,
                    //Weight = node.Weight,
                });
            }
        }
    }
#endif

    public GridNode GetNode(int x, int y)
    {
        if (x < 0 || x >= gridNodes.GetLength(0) || y < 0 || y >= gridNodes.GetLength(1))
            throw new System.IndexOutOfRangeException("Grid node indices out of range");

        return gridNodes[x, y];
    }

    private void OnDrawGizmos()
    {
        if (gridNodes == null || GridSettings == null) return;

        Gizmos.color = Color.green;
        for (int x = 0; x < gridNodes.GetLength(0); ++x)
        {
            for (int y = 0; y < gridNodes.GetLength(1); ++y)
            {
                GridNode node = gridNodes[x, y];
                Gizmos.color = node.GizmoColor;
                Gizmos.DrawWireCube(node.WorldPosition, Vector3.one * GridSettings.NodeSize * 0.9f);
            }
        }
    }

    [CustomEditor(typeof(GridManager))]
    public class GridManagerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            
            GridManager grid = (GridManager)target;
            if (grid.IsInitialized)
            {
                if(GUILayout.Button("Refresh Grid Debug View"))
                {
                    grid.PopulateDebugList();
                }
            }
        }
    }
}
