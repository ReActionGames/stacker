using System;
using System.Collections;
using System.Collections.Generic;
using HenderStudios.Events;
using Stacker.Cells;
using UnityEngine;

namespace Stacker
{
    public class FullRowsDeleter : MonoBehaviour
    {
        private TetroGrid grid;
        
        public void DeleteRows(TetroGrid grid, Cell[,] cells)
        {
            this.grid = grid;
            List<int> fullRowIndexs = new List<int>();
            int[][] cellMoveDistance = GetDistanceArray(cells.GetLength(0), cells.GetLength(1));

            for (int i = 0; i < cells.GetLength(1); i++)
            {
                fullRowIndexs.Add(i);
                for (int j = 0; j < cells.GetLength(0); j++)
                {
                    if (grid.IsCellEmpty(j, i))
                    {
                        fullRowIndexs.Remove(i);
                        break;
                    }
                }
            }
            if (fullRowIndexs.Count <= 0)
            {
                EventManager.TriggerEvent(EventNames.GridFinishedUpdating, new Message(0.0f));
                return /*false*/; 
            }

            foreach (int row in fullRowIndexs)
            {
                DeleteRow(row);
                cellMoveDistance = AddOneAboveRow(cellMoveDistance, row);
            }
            EventManager.TriggerEvent(EventNames.RowCompleted, new Message(fullRowIndexs.Count));

            StartCoroutine(WaitAndMoveCells(grid.GameSettings.RowFallDelay, cellMoveDistance));
            //return true;
        }

        private IEnumerator WaitAndMoveCells(float waitTime, int[][] cellMoveDistance)
        {
            yield return new WaitForSeconds(waitTime);
            MoveCells(cellMoveDistance);
        }

        private void MoveCells(int[][] cellMoveDistance)
        {
            for (int i = 0; i < cellMoveDistance.Length; i++)
            {
                for (int j = 0; j < cellMoveDistance[i].Length; j++)
                {
                    if (cellMoveDistance[i][j] > 0)
                        grid.MoveCell(i, j, cellMoveDistance[i][j]);
                }
            }
            EventManager.TriggerEvent(EventNames.GridFinishedUpdating, new Message(grid.GameSettings.RowFallSpeed));
        }

        private int[][] AddOneAboveRow(int[][] array, int row)
        {
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = row + 1; j < array[i].Length; j++)
                {
                    if (grid.IsCellFull(i, j))
                        array[i][j] += 1;
                }
            }
            return array;
        }

        private static int[][] GetDistanceArray(int sizeX, int sizeY)
        {
            int[][] array = new int[sizeX][];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = new int[sizeY];
            }
            return array;
        }

        private void DeleteRow(int row)
        {
            Cell[] cells = grid.GetCellsInRow(row);
            foreach (Cell cell in cells)
            {
                grid.RemoveCell(cell);
            }
        }
    }
}