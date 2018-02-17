using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stacker.Cells
{
    public class DyingCell : CellState
    {
        public override void OnEnterState(Cell cell)
        {
            base.OnEnterState(cell);
            cell.GetComponent<Animator>().SetTrigger("Die");
            
            //cell.ChangeState(new InactiveCell());
        }
    }
}