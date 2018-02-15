using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stacker.Cells
{
    public class MovingCellPool : MonoBehaviour
    {
        [SerializeField] private Transform poolPos;
        [SerializeField] private MovingCell movingCellPrefab;

        private Stack<MovingCell> pool;

        public Stack<MovingCell> PreWarm(int size)
        {
            pool = new Stack<MovingCell>();
            for (int i = 0; i < size; i++)
            {
                MovingCell cell = Instantiate(movingCellPrefab, poolPos.position, Quaternion.identity);
                cell.name = $"moving cell {i,0:000}";
                cell.transform.parent = transform;
                cell.SetPool(this);
                pool.Push(cell);
            }
            return pool;
        }

        public void MoveCell(TetroGrid grid, Cell cell, Vector2 startPos, int moveDistance)
        {
            MovingCell movingCell = pool.Pop();
            movingCell.MoveDown(grid, cell, startPos, moveDistance);
        }

        public void ReturnCell(MovingCell cell)
        {
            cell.transform.parent = transform;
            cell.transform.localPosition = Vector3.zero;
            pool.Push(cell);
        }
    }
}