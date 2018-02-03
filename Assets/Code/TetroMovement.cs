using HenderStudios.Events;
using Stacker.ScriptableObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stacker
{
    public class TetroMovement : MonoBehaviour
    {
        private bool falling = false;
        private Tetro tetro;
        private TetroGrid grid;

        private Tetro Tetro
        {
            get
            {
                if (tetro == null)
                    tetro = GetComponent<Tetro>();
                return tetro;
            }
        }
        private TetroGrid Grid
        {
            get
            {
                if (grid == null)
                    grid = FindObjectOfType<TetroGrid>();
                return grid;
            }
        }
        private TetroSettings Settings
        {
            get
            {
                return Grid.TetroSettings;
            }
        }

        private void Start()
        {
            falling = false;
            EventManager.StartListening(EventNames.TetroMoveRight, ShiftRight);
            EventManager.StartListening(EventNames.TetroMoveLeft, ShiftLeft);
            EventManager.StartListening(EventNames.TetroMoveDrop, Drop);
            EventManager.StartListening(EventNames.TetroRotateClockwise, RotateClockwise);
            EventManager.StartListening(EventNames.TetroRotateCounterClockwise, RotateCounterClockwise);
        }

        private void ShiftRight(Message msg)
        {
            throw new NotImplementedException();
        }

        private void ShiftLeft(Message msg)
        {
            throw new NotImplementedException();
        }

        private void Drop(Message msg)
        {
            throw new NotImplementedException();
        }

        private void RotateClockwise(Message arg0)
        {
            throw new NotImplementedException();
        }

        private void RotateCounterClockwise(Message arg0)
        {
            throw new NotImplementedException();
        }
        
        private IEnumerator FallOneCell()
        {
            Snap();

            falling = true;
            Vector3 startPos = transform.position;
            Vector2 nextCellPos = Grid.GetCellPosBelow(transform.position);
            float time = 0;
            while (time < Settings.FallingSpeed)
            {
                time += Time.deltaTime;
                float perc = time / Settings.FallingSpeed;
                float yPos = Vector3.Lerp(startPos, nextCellPos, perc).y;
                transform.position = new Vector3(transform.position.x, yPos);
                yield return null;
            }
            Snap();
            falling = false;
            AfterFall();
        }

        private void AfterFall()
        {
            if (CanFall())
                StartCoroutine(FallOneCell());
            else
            {
                Snap();
                Die();
            }
        }

        private bool CanFall()
        {
            foreach (var tile in Tetro.Tiles)
            {
                if (Grid.IsCellFull(Grid.GetCellPosBelow(tile.transform.position)))
                    return false;
            }
            return true;
        }

        public void Snap()
        {
            FindObjectOfType<SnapToGrid>().Snap();
        }

        //private void ShiftRight()
        //{
        //    StartCoroutine(Shift(grid.Grid.cellSize.x));
        //}

        //private void ShiftLeft()
        //{
        //    StartCoroutine(Shift(grid.Grid.cellSize.x * -1));
        //}

        //private IEnumerator Shift(float newXPos)
        //{
        //    SnapXPos();

        //    Vector3 startPos = tetro.transform.position;

        //    Vector2 nextCellPos = newXPos > 0 ? grid.GetCellPosRight(startPos) : grid.GetCellPosLeft(startPos);
        //    Vector2 endPos = new Vector2(nextCellPos.x, tetro.transform.position.y);

        //    if (grid.IsOutOfBounds(endPos))
        //        yield break;

        //    float time = 0;
        //    while (time < shiftSpeed)
        //    {
        //        time += Time.deltaTime;
        //        float perc = time / shiftSpeed;
        //        float xPos = Vector3.Lerp(startPos, endPos, perc).x;
        //        tetro.transform.position = new Vector3(xPos, tetro.transform.position.y);
        //        yield return null;
        //    }

        //    SnapXPos();
        //}

        //private void SnapXPos()
        //{
        //    Vector2 cellPos = grid.GetCellPosAt(tetro.transform.position);
        //    tetro.transform.position = new Vector3(cellPos.x, tetro.transform.position.y);
        //}
    }
}