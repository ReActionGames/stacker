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
            if (tetro.Active && CanMoveRight())
                StartCoroutine(LerpShift(grid.Grid.cellSize.x));
        }

        private void ShiftLeft(Message msg)
        {
            if (tetro.Active && CanMoveLeft())
            {
                StartCoroutine(LerpShift(grid.Grid.cellSize.x * -1));
            }
        }
        
        private IEnumerator LerpShift(float newXPos)
        {
            Snap(SnapAxis.X);

            Vector3 startPos = tetro.transform.position;
            //Debug.Log("Start Pos: " + startPos);

            Vector2 nextCellPos = newXPos > 0 ? grid.GetCellPosRight(startPos) : grid.GetCellPosLeft(startPos);
            //Debug.Log("Next Cell Pos: " + nextCellPos);

            Vector2 endPos = new Vector2(nextCellPos.x, transform.position.y);
            //Debug.Log("End Pos: " + endPos);

            //if (grid.IsOutOfBounds(endPos))
            //{
            //    Debug.LogWarning("End Pos is out of bounds! Ending shift...");
            //    yield break;
            //}

            float time = 0;
            while (tetro.Active /*&& tetro.Falling*/ && time < settings.ShiftSpeed)
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