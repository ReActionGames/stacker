using Stacker.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stacker.Cells
{
    public class ActiveCell : CellState
    {
        public TetroType Type
        {
            get;
            private set;
        }
        public Color Color
        {
            get;
            private set;
        }

        public bool HasCoin
        {
            get;
            private set;
        }

        public ActiveCell(TetroType tetroType)
        {
            Type = tetroType;
        }

        public ActiveCell(Color color)
        {
            Type = TetroType.None;
            Color = color;
        }

        public override void OnEnterState(Cell cell)
        {
            if (Type == TetroType.None)
            {
                SetColor(cell, Color);
                return;
            }
            SetColor(cell, cell.grid.ColorPalette.GetColorBasedOnType(Type));
        }

        private void SetColor(Cell cell, Color color)
        {
            cell.sprite.color = color;
        }

        public void ActivateCoin(Cell cell)
        {
            cell.CoinImage.SetActive(true);
            HasCoin = true;
        }
    }
}