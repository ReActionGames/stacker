using Sirenix.OdinInspector;
using Stacker.Cells;
using Stacker.ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace Stacker
{
    public partial class TetroGrid : MonoBehaviour
    {

        [SerializeField]
        private Vector2 gridSize;
        [SerializeField, OnValueChanged("UpdateGridCellSize")]
        private Vector3 cellSize;
        [SerializeField]
        private Grid grid;
        [SerializeField, Required]
        private Cell cellPrefab;
        [SerializeField]
        private TetroColorPalette colorPalette;

        [SerializeField, BoxGroup("Gizmos")]
        private bool showCoordinates;
        [SerializeField, BoxGroup("Gizmos")]
        private bool negativeX, negativeY;


        [SerializeField, BoxGroup("Events")]
        public UnityEvent OnGridUpdated;

        /// <summary>
        /// A binary representation of the cells. 0 means the cell is empty. 1 means it's full.
        /// </summary>
        private Cell[,] cells;

        public Grid Grid
        {
            get
            {
                return grid;
            }
        }

        public TetroColorPalette ColorPalette
        {
            get
            {
                return colorPalette;
            }
        }

        private void Awake()
        {
            InitializeCellArray();
            if (grid == null)
                grid = gameObject.AddComponent<Grid>();
        }

        private void InitializeCellArray()
        {
            cells = new Cell[(int)gridSize.x, (int)gridSize.y];
            for (int x = 0; x < cells.GetLength(0); x++)
            {
                GameObject row = new GameObject("row" + x);
                row.transform.SetParent(transform);
                for (int y = 0; y < cells.GetLength(1); y++)
                {
                    Cell cell = Instantiate(cellPrefab, grid.GetCellCenterWorld(new Vector3Int(x, y, 0)), Quaternion.identity, row.transform);
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

        public bool IsCellFull(Vector2 cellPos)
        {
            Vector3Int pos = grid.WorldToCell(cellPos);
            if (IsOutOfBounds(pos))
                return true;
            return cells[pos.x, pos.y].CurrentState is ActiveCell;
        }

        public bool IsCellEmpty(Vector2 cellPos)
        {
            Vector3Int pos = grid.WorldToCell(cellPos);
            if (IsOutOfBounds(pos))
                return false;
            return !(cells[pos.x, pos.y].CurrentState is ActiveCell);
        }

        public void SetCellFull(Vector2 pos, Enums.TetroType type)
        {
            Vector3Int cellPos = grid.WorldToCell(pos);
            if (IsOutOfBounds(cellPos))
                return;
            cells[cellPos.x, cellPos.y].ChangeState(new ActiveCell(type));
            OnGridUpdated?.Invoke();
        }

        public void SetCellEmtpy(Vector2 pos)
        {
            Vector3Int cellPos = grid.WorldToCell(pos);
            if (IsOutOfBounds(cellPos))
                return;
            cells[cellPos.x, cellPos.y].ChangeState(new InactiveCell());
            OnGridUpdated?.Invoke();
        }

        public Vector2 GetCellBelow(Vector2 pos)
        {
            return grid.GetCellCenterWorld(grid.WorldToCell(pos) + Vector3Int.down);
            //return Vector2.down;
        }

        public Vector2 GetCellAt(Vector2 pos)
        {
            return grid.GetCellCenterWorld(grid.WorldToCell(pos));
        }

        private bool IsOutOfBounds(Vector3Int cell)
        {
            return cell.x < 0 || cell.x >= cells.GetLength(0) || cell.y < 0 || cell.y >= cells.GetLength(1);
        }

        #region Editor

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
                    DrawCell(x, y);
                    if (showCoordinates)
                        DrawCoordinates(x, y);
                }
            }
        }

        private void DrawCell(int x, int y)
        {
            Gizmos.color = Color.green;
            Vector3 pos = GetCellCenter(x, y);
            Gizmos.DrawWireCube(pos, cellSize);

            if (cells != null && IsCellFull(pos))
            {
                Gizmos.color = new Color(0, 1, 0, 0.25f);
                Gizmos.DrawCube(pos, cellSize);
            }
        }

        private void DrawCoordinates(int x, int y)
        {
            Vector3 pos = GetCellCenter(x, y);
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