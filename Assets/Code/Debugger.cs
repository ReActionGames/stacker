using Stacker.Cells;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stacker
{
    public class Debugger : MonoBehaviour
    {

        private void Update()
        {
            if (Input.GetMouseButtonUp(0))
            {
                UpdateGrid();
                return;
            }
            if (Input.GetMouseButton(0))
            {
                EditGrid();
                return;
            }
        }

        private void UpdateGrid()
        {
            FindObjectOfType<TetroGrid>().DeleteFullRows();
        }

        private void EditGrid()
        {
            TetroGrid grid = FindObjectOfType<TetroGrid>();
            Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                DeactivateCell(position);
                return;
            }
            ActivateCell(position);
        }

        private void DeactivateCell(Vector2 position)
        {
            FindObjectOfType<TetroGrid>().SetCellEmtpy(position);
        }

        private void ActivateCell(Vector2 position)
        {
            FindObjectOfType<TetroGrid>().SetCellFull(position, Enums.TetroType.I);
        }
    }
}