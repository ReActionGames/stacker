using HenderStudios.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stacker.Tetros
{
    public class Shift : MovementController
    {
        public override void SetUp(TetroMovement movement)
        {
            base.SetUp(movement);
            EventManager.StartListening(EventNames.TetroMoveRight, ShiftRight);
            EventManager.StartListening(EventNames.TetroMoveLeft, ShiftLeft);
        }
        
        private void ShiftRight(Message msg)
        {
            if (CanMoveRight())
                StartCoroutine(LerpShift(grid.Grid.cellSize.x));
        }

        private void ShiftLeft(Message msg)
        {
            if (CanMoveLeft())
                StartCoroutine(LerpShift(grid.Grid.cellSize.x * -1));
        }
        
        private IEnumerator LerpShift(float newXPos)
        {
            Snap(SnapAxis.X);

            Vector3 startPos = tetro.transform.position;

            Vector2 nextCellPos = newXPos > 0 ? grid.GetCellPosRight(startPos) : grid.GetCellPosLeft(startPos);
            Vector2 endPos = new Vector2(nextCellPos.x, transform.position.y);

            if (grid.IsOutOfBounds(endPos))
                yield break;

            float time = 0;
            while (tetro.Active && time < settings.ShiftSpeed)
            {
                time += Time.deltaTime;
                float perc = time / settings.ShiftSpeed;
                float xPos = Vector3.Lerp(startPos, endPos, perc).x;
                transform.position = new Vector3(xPos, transform.position.y);
                yield return null;
            }

            Snap(SnapAxis.X);
        }
    }
}