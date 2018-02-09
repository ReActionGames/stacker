using UnityEngine;
using System.Collections;
using Stacker.ScriptableObjects;

namespace Stacker.Tetros
{
    public abstract class MovementController : MonoBehaviour
    {
        protected Tetro tetro;
        protected Tile[] tiles;
        protected TetroGrid grid;
        protected TetroSettings settings;
        protected TetroMovement movement;
        
        protected enum SnapAxis
        {
            X,
            Y,
            Both
        }

        public virtual void SetUp(TetroMovement movement)
        {
            this.movement = movement;
            tetro = movement.Tetro;
            tiles = tetro.Tiles;
            grid = movement.Grid;
            settings = movement.Settings;
        }
        
        protected void Snap(SnapAxis axis)
        {
            Vector2 cellPos = grid.GetCellPosAt(transform.position);
            Vector2 newPos = transform.position;
            switch (axis)
            {
                case SnapAxis.X:
                    newPos = new Vector2(cellPos.x, transform.position.y);
                    break;
                case SnapAxis.Y:
                    newPos = new Vector2(transform.position.x, cellPos.y);
                    break;
                case SnapAxis.Both:
                    newPos = cellPos;
                    break;
            }
            transform.position = newPos;
        }

        protected void StopAllMovement()
        {
            movement.Stop();
        }

        protected bool CanFall()
        {
            foreach (var tile in tiles)
            {
                if (tile.CanMove(grid, Tile.Side.Down) == false)
                    return false;
            }
            return true;
        }

        protected bool CanFall(Vector3 offset)
        {
            foreach (var tile in tiles)
            {
                if (tile.CanMove(grid, Tile.Side.Down, offset) == false)
                    return false;
            }
            return true;
        }

        protected bool CanMoveRight()
        {
            foreach (var tile in tiles)
            {
                if (tile.CanMove(grid, Tile.Side.Right) == false)
                    return false;
            }
            return true;
        }

        protected bool CanMoveRight(Vector3 offset)
        {
            foreach (var tile in tiles)
            {
                if (tile.CanMove(grid, Tile.Side.Right, offset) == false)
                    return false;
            }
            return true;
        }

        protected bool CanMoveLeft()
        {
            foreach (var tile in tiles)
            {
                if (tile.CanMove(grid, Tile.Side.Left) == false)
                    return false;
            }
            return true;
        }

        protected bool CanMoveLeft(Vector3 offset)
        {
            foreach (var tile in tiles)
            {
                if (tile.CanMove(grid, Tile.Side.Left, offset) == false)
                    return false;
            }
            return true;
        }

        protected bool CanRotate(int rotationIndex)
        {
            Vector2[] positions = tetro.GetRotationPositions(rotationIndex);
            foreach (Vector2 pos in positions)
            {
                if (grid.IsCellFull(pos) == true)
                    return false;
            }
            return true;
        }
    }
}