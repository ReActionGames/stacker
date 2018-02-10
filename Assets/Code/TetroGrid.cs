using Sirenix.OdinInspector;
using Stacker.Cells;
using Stacker.ScriptableObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace Stacker
{
    public partial class TetroGrid : MonoBehaviour
    {
        [SerializeField] private Vector2 gridSize;
        [OnValueChanged("UpdateGridCellSize")]
        [SerializeField]
        private Vector3 cellSize;
        [SerializeField] private Grid grid;
        [Required]
        [SerializeField]
        private Cell cellPrefab;
        [SerializeField] private TetroColorPalette colorPalette;
        [SerializeField] private TetroSettings tetroSettings;

        [BoxGroup("Gizmos")]
        [SerializeField]
        private bool showGizmos, showCoordinates;
        [BoxGroup("Gizmos")]
        [SerializeField]
        private bool negativeX, negativeY;

        [BoxGroup("Events")]
        [SerializeField]
        private UnityEvent onGridUpdated;

        /// <summary>
        /// A 2D array of Cells. Cells are either null, Active, Inactive, Moving, or Dying.
        /// </summary>
        private Cell[,] cells;
        private FullRowsDeleter rowsDeleter;

        public Grid Grid
        {
            get
            {
                return grid;
            }
        }
        public UnityEvent OnGridUpdated
        {
            get
            {
                return onGridUpdated;
            }
        }
        public TetroColorPalette ColorPalette
        {
            get
            {
                return colorPalette;
            }
        }
        public TetroSettings TetroSettings
        {
            get
            {
                return tetroSettings;
            }
        }

        private void Awake()
        {
            if (grid == null)
            {
                grid = GetComponent<Grid>();
                if (grid == null)
                    grid = gameObject.AddComponent<Grid>();
            }
            if (rowsDeleter == null)
            {
                rowsDeleter = GetComponent<FullRowsDeleter>();
                if (rowsDeleter == null)
                    rowsDeleter = gameObject.AddComponent<FullRowsDeleter>();
            }

            InitializeCellArray();
        }

        private void InitializeCellArray()
        {
            cells = new Cell[(int)gridSize.x, (int)gridSize.y];
            for (int x = 0; x < cells.GetLength(0); x++)
            {
                GameObject col = new GameObject("col" + x);
                col.transform.SetParent(transform);
                for (int y = 0; y < cells.GetLength(1); y++)
                {
                    Cell cell = Instantiate(cellPrefab, grid.GetCellCenterWorld(new Vector3Int(x, y, 0)), Quaternion.identity, col.transform);
                    cell.name = $"cell ({x}, {y})";
                    cell.SetGrid(this);
                    cells[x, y] = cell;
                }
            }
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

        public void DeleteFullRows()
        {
            rowsDeleter.DeleteRows(this, cells);
        }

        public Cell[] GetCellsInRow(int row)
        {
            Cell[] temp = new Cell[cells.GetLength(0)];
            for (int i = 0; i < temp.Length; i++)
            {
                temp[i] = cells[i, row];
            }
            return temp;
        }

        #region Cell Checkers and Setters

        public bool IsCellFull(Vector2 worldCellPos)
        {
            Vector3Int pos = grid.WorldToCell(worldCellPos);
            if (IsOutOfBounds(pos))
                return true;
            if (IsTooHigh(pos))
                return false;
            return GetCellAt(worldCellPos).CurrentState is ActiveCell;
        }

        public bool IsCellFull(int x, int y)
        {
            if (x >= cells.GetLength(0) || y >= cells.GetLength(1) || x < 0 || y < 0)
                return true;
            return cells[x, y].CurrentState is ActiveCell;
        }

        public bool IsCellEmpty(Vector2 worldCellPos)
        {
            Vector3Int pos = grid.WorldToCell(worldCellPos);
            if (IsOutOfBounds(pos))
                return false;
            if (IsTooHigh(pos))
                return true;
            return !(cells[pos.x, pos.y].CurrentState is ActiveCell);
        }

        public bool IsCellEmpty(int x, int y)
        {
            if (x >= cells.GetLength(0) || y >= cells.GetLength(1) || x < 0 || y < 0)
                return true;
            return !(cells[x, y].CurrentState is ActiveCell);
        }

        public void SetCellFull(Vector2 worldCellPos, Enums.TetroType type)
        {
            Vector3Int cellPos = grid.WorldToCell(worldCellPos);
            if (IsOutOfBounds(cellPos) || IsTooHigh(cellPos))
                return;
            cells[cellPos.x, cellPos.y].ChangeState(new ActiveCell(type));
            OnGridUpdated?.Invoke();
        }

        public void SetCellEmtpy(Vector2 worldCellPos)
        {
            Vector3Int cellPos = grid.WorldToCell(worldCellPos);
            if (IsOutOfBounds(cellPos) || IsTooHigh(cellPos))
                return;
            cells[cellPos.x, cellPos.y].ChangeState(new InactiveCell());
            OnGridUpdated?.Invoke();
        }

        public void RemoveCell(Cell cell)
        {
            cell.ChangeState(new DyingCell());
        }

        public Vector2 GetCellPosBelow(Vector2 pos)
        {
            return grid.GetCellCenterWorld(grid.WorldToCell(pos) + Vector3Int.down);
            //return Vector2.down;
        }

        public Vector2 GetCellPosRight(Vector2 pos)
        {
            return grid.GetCellCenterWorld(grid.WorldToCell(pos) + Vector3Int.right);
            //return Vector2.down;
        }

        public Vector2 GetCellPosLeft(Vector2 pos)
        {
            return grid.GetCellCenterWorld(grid.WorldToCell(pos) + Vector3Int.left);
            //return Vector2.down;
        }

        public Cell GetCellAt(Vector2 pos)
        {
            var intPos = pos.ToVector2Int();
            return cells[intPos.x, intPos.y];
        }

        public Vector2 GetCellPosAt(Vector2 pos)
        {
            return grid.GetCellCenterWorld(grid.WorldToCell(pos));
        }

        private bool IsTooHigh(Vector3Int cell)
        {
            return cell.y >= cells.GetLength(1);
        }

        public bool IsOutOfBounds(Vector3Int cell)
        {
            return cell.x < 0 || cell.x >= cells.GetLength(0) || cell.y < 0 /*|| cell.y >= cells.GetLength(1)*/;
        }

        public bool IsOutOfBounds(Vector2 cellPos)
        {
            return IsOutOfBounds(grid.WorldToCell(cellPos));
        }
        #endregion

        #region Editor

        private void OnDrawGizmos()
        {
            if (showGizmos == false)
                return;
            for (int col = 0; col < gridSize.x; col++)
            {
                for (int row = 0; row < gridSize.y; row++)
                {
                    DrawCell(col, row);
                    if (showCoordinates)
                        DrawCoordinates(col, row);
                }
            }
        }

        private void DrawCell(int row, int col)
        {
            Gizmos.color = Color.green;
            Vector3 pos = GetCellCenter(row, col);
            Gizmos.DrawWireCube(pos, cellSize);

            if (cells != null)
            {
                if (IsCellFull(pos))
                {
                    Gizmos.color = new Color(0, 1, 0, 0.25f);
                    Gizmos.DrawCube(pos, cellSize);
                }
                else if (GetCellAt(pos).CurrentState is DyingCell)
                {
                    Gizmos.color = new Color(1, 0, 0, 0.75f);
                    Gizmos.DrawCube(pos, cellSize);
                }
            }
        }

        private void DrawCoordinates(int row, int col)
        {
            Vector3 pos = GetCellCenter(row, col);
            Vector3 labelPos = new Vector2(pos.x - (cellSize.x / 4), pos.y + (cellSize.y / 4));
            Vector3 text = grid.WorldToCell(pos);
            Handles.Label(labelPos, $"({text.x}, {text.y})");
        }

        private Vector3 GetCellCenter(int x, int y)
        {
            Vector3 pos;
            pos = new Vector2(x, y);
            pos += transform.position;
            pos *= cellSize.x;
            pos = grid.GetCellCenterWorld(grid.WorldToCell(pos));
            return pos;
        }

        #endregion
    }
}