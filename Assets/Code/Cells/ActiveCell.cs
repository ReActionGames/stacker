using Stacker.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
            SetTexture(cell);
            if (Type == TetroType.None)
            {
                SetColor(cell, Color);
                return;
            }
            SetColor(cell, cell.Grid.ColorPalette.GetColorBasedOnType(Type));
        }

        private void SetColor(Cell cell, Color color)
        {
            cell.Sprite.color = color;
        }

        private void SetTexture(Cell cell)
        {
            cell.Texture.gameObject.SetActive(true);
            ScriptableObjects.Theme theme = cell.Grid.TetroSettings.Theme;
            cell.Texture.sprite = theme.Sprite;
            cell.Texture.color = theme.Color;
            cell.Texture.type = Image.Type.Simple;
            cell.Texture.preserveAspect = true;
        }

        public void ActivateCoin(Cell cell)
        {
            cell.CoinImage.SetActive(true);
            HasCoin = true;
        }
    }
}