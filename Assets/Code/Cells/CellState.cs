using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stacker.Cells
{
    public abstract class CellState
    {
        public virtual void OnEnterState(Cell cell)
        {

        }

        public virtual void OnExitState(Cell cell)
        {
        }
    }
}