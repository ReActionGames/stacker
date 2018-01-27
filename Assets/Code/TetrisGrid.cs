using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public partial class TetrisGrid : MonoBehaviour
{

    [SerializeField]
    private Vector2 gridSize;
    [SerializeField, OnValueChanged("UpdateGridCellSize")]
    private Vector3 cellSize;
    [SerializeField]
    private Grid grid;
    [SerializeField]
    private CellsContainer cellsContainer;

    [SerializeField, BoxGroup("Gizmos")]
    private bool showCoordinates;
    [SerializeField, BoxGroup("Gizmos")]
    private bool negativeX, negativeY;


    [SerializeField, BoxGroup("Events")]
    public UnityEvent OnGridUpdated;

    /// <summary>
    /// A binary representation of the cells. 0 means the cell is empty. 1 means it's full.
    /// </summary>
    private int[,] cells
    {
        get
        {
            if (cellsContainer == null)
                cellsContainer = gameObject.AddComponent<CellsContainer>();
            return cellsContainer.Cells;
        }
        set
        {
            if (cellsContainer == null)
                cellsContainer = gameObject.AddComponent<CellsContainer>();
            cellsContainer.Cells = value;
        }
    }

    public Grid Grid
    {
        get
        {
            return grid;
        }
    }

    private void Awake()
    {
        cells = new int[(int)gridSize.x, (int)gridSize.y];
        if (grid == null)
            grid = gameObject.AddComponent<Grid>();
    }

    private void Start()
    {
        UpdateGridCellSize();
    }

    [Button]
    public void UpdateGrid()
    {
        OnGridUpdated?.Invoke();
    }

    private void UpdateGridCellSize()
    {
        grid.cellSize = cellSize;
    }

    public bool IsCellEmpty(Vector2 cellPos)
    {
        Vector3Int pos = grid.WorldToCell(cellPos);
        //if(pos.)
        if (IsOutOfBounds(pos))
            return false;
        return cells[pos.x, pos.y] == 0;
    }

    public void SetCellFull(Vector2 pos)
    {
        Vector3Int cellPos = grid.WorldToCell(pos);
        if (IsOutOfBounds(cellPos))
            return;
        cells[cellPos.x, cellPos.y] = 1;
        OnGridUpdated?.Invoke();
    }

    public void SetCellEmtpy(Vector2 pos)
    {
        Vector3Int cellPos = grid.WorldToCell(pos);
        if (IsOutOfBounds(cellPos))
            return;
        cells[cellPos.x, cellPos.y] = 0;
        OnGridUpdated?.Invoke();
    }

    public Vector2 GetCellBelow(Vector2 pos)
    {
        return grid.GetCellCenterWorld(grid.WorldToCell(pos) + Vector3Int.down);
        //return Vector2.down;
    }

    private void OnDrawGizmos()
    {
        int x = 0;
        if (negativeX)
            x = (int)-gridSize.x;

        for (; x < gridSize.x; x++)
        {
            int y = 0;
            if (negativeY)
                y = (int)-gridSize.y;

            for (; y < gridSize.y; y++)
            {
                Gizmos.color = Color.green;

                Vector3 pos;
                pos = new Vector2(x, y);
                pos += transform.position;
                pos *= cellSize.x;
                pos = grid.GetCellCenterWorld(grid.WorldToCell(pos));
                Gizmos.DrawWireCube(pos, cellSize);

                if (showCoordinates == false)
                    continue;
                Vector3 labelPos = new Vector2(pos.x - (cellSize.x / 4), pos.y + (cellSize.y / 4));
                //labelPos = pos;
                //labelPos += (Vector2)cellSize;
                Vector3 text = grid.WorldToCell(pos);
                Handles.Label(labelPos, $"({text.x}, {text.y})");
            }
        }
    }

    private bool IsOutOfBounds(Vector3Int cell)
    {
        return cell.x < 0 || cell.x >= cells.GetLength(0) || cell.y < 0 || cell.y >= cells.GetLength(1);
    }

}
