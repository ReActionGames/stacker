using System;
using System.Collections;
using System.Collections.Generic;
using Stacker.Cells;
using UnityEngine;

namespace Stacker
{
    public class FullRowsDeleter : MonoBehaviour
    {


        public void DeleteRows(TetroGrid grid, Cell[,] cells)
        {
            List<int> fullRowIndexs = new List<int>();

            for (int i = 0; i < cells.GetLength(1); i++)
            {
                fullRowIndexs.Add(i);
                for (int j = 0; j < cells.GetLength(0); j++)
                {
                    if (grid.IsCellEmpty(j, i))
                        fullRowIndexs.Remove(i);
                }
            }
            foreach (int row in fullRowIndexs)
            {
                DeleteRow(grid, row);
            }
        }

        private void DeleteRow(TetroGrid grid, int row)
        {
            Cell[] cells = grid.GetCellsInRow(row);
            foreach (Cell cell in cells)
            {
                grid.RemoveCell(cell);
            }
        }
    }
}