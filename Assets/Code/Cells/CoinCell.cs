using System.Collections;
using System.Collections.Generic;
using Stacker.Enums;
using UnityEngine;

namespace Stacker.Cells
{
    public class CoinCell : ActiveCell
    {
        public CoinCell(TetroType tetroType) : base(tetroType)
        {
        }

        public CoinCell(Color color) : base(color)
        {
        }

        public override void OnEnterState(Cell cell)
        {
            base.OnEnterState(cell);
            cell.CoinImage.SetActive(true);
        }

        public override void OnExitState(Cell cell)
        {
            base.OnExitState(cell);
            cell.CoinImage.SetActive(false);
        }
    }
}