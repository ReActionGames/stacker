using Stacker.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stacker.Cells
{
    public class ActiveCell : CellState
    {
        private TetroType type;

        public ActiveCell(TetroType tetroType)
        {
            type = tetroType;
        }

        public override void OnEnterState(Cell cell)
        {
            cell.sprite.color = cell.grid.ColorPalette.GetColorBasedOnType(type);
        }
    }
}