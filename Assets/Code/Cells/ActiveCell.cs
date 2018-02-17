using Stacker.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stacker.Cells
{
    public class ActiveCell : CellState
    {
        private TetroType type;
        private Color _color;

        public ActiveCell(TetroType tetroType)
        {
            type = tetroType;
        }

        public ActiveCell(Color color)
        {
            type = TetroType.None;
            _color = color;
        }

        public override void OnEnterState(Cell cell)
        {
            if (type == TetroType.None)
            {
                SetColor(cell, _color);
                return;
            }
            SetColor(cell, cell.grid.ColorPalette.GetColorBasedOnType(type));
        }

        private void SetColor(Cell cell, Color color)
        {
            cell.sprite.color = color;
        }
    }
}