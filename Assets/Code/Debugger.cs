using HenderStudios.Events;
using Sirenix.OdinInspector;
using Stacker.Cells;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stacker
{
    public class Debugger : MonoBehaviour
    {
        [SerializeField] private bool updateGrid = false;

        [Button]
        private void Add200Coins()
        {
            Currency.AddCoins(200);
        }

        private void Update()
        {
            if (!updateGrid)
                return;
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
            EventManager.TriggerEvent(EventNames.TetroEndFalling);
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