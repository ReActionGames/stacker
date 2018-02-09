using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stacker.Cells
{
    public class InactiveCell : CellState
    {
        public override void OnEnterState(Cell cell)
        {
            cell.sprite.color = new Color(0, 0, 0, 0f);
        }
    }
}