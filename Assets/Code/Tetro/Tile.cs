using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stacker.Tetros
{
    public class Tile : MonoBehaviour
    {
        public enum Side
        {
            Right,
            Left,
            Down
        }

        [SerializeField] private GameObject coinImage;

        private bool hasCoin;

        /// <summary>
        /// Checks whether the cell on the side "side" is empty. If it is, return true. Otherwise, return false.
        /// </summary>
        /// <param name="grid">The grid to check with.</param>
        /// <param name="side">The side of the tile to check.</param>
        /// <returns></returns>
        public bool CanMove(TetroGrid grid, Side side)
        {
            switch (side)
            {
                case Side.Right:
                    return grid.IsCellEmpty(grid.GetCellPosRight(transform.position));
                case Side.Left:
                    return grid.IsCellEmpty(grid.GetCellPosLeft(transform.position));
                case Side.Down:
                    return grid.IsCellEmpty(grid.GetCellPosBelow(transform.position));
            }
            return false;
        }

        /// <summary>
        /// Checks whether the cell on the side "side" is empty with the given offset. If it is, return true. Otherwise, return false.
        /// </summary>
        /// <param name="grid">The grid to check with.</param>
        /// <param name="side">The side of the tile to check.</param>
        /// <param name="offset">The offset to apply.</param>
        /// <returns></returns>
        public bool CanMove(TetroGrid grid, Side side, Vector3 offset)
        {
            Vector3 pos = transform.position + offset;
            switch (side)
            {
                case Side.Right:
                    return grid.IsCellEmpty(grid.GetCellPosRight(pos));
                case Side.Left:
                    return grid.IsCellEmpty(grid.GetCellPosLeft(pos));
                case Side.Down:
                    return grid.IsCellEmpty(grid.GetCellPosBelow(pos));
            }
            return false;
        }

        public void SetColor(Enums.TetroType type)
        {
            var grid = FindObjectOfType<TetroGrid>();
            GetComponent<SpriteRenderer>().color = grid.ColorPalette.GetColorBasedOnType(type);
        }

        public void SetHasCoin(bool hasCoin)
        {
            this.hasCoin = hasCoin;
            coinImage.SetActive(hasCoin);
        }
    }
}