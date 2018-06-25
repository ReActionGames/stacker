using HenderStudios.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stacker.Tetros
{
    public class Drop : MovementController
    {

        public override void SetUp(TetroMovement movement)
        {
            base.SetUp(movement);
            EventManager.StartListening(EventNames.TetroMoveDrop, DropTetro);
        }

        private void DropTetro(Message msg)
        {
            if (!tetro.Active)
                return;
            Vector3 offset = Vector3.zero;
            while (CanFall(offset))
            {
                offset += Vector3.down;
            }
            StopAllMovement();
            Snap(SnapAxis.X);
            Vector3 dropPos = transform.position + offset;
            Vector2 cellPos = grid.GetCellPosAt(dropPos);
            EventManager.TriggerEvent(EventNames.SuccessfulDrop, new Message(-offset.y));
            StartCoroutine(LerpDrop(cellPos));
            //tetro.Falling = false;
        }

        private IEnumerator LerpDrop(Vector3 endPos)
        {
            Vector3 startPos = tetro.transform.position;

            //if (grid.IsOutOfBounds(endPos))
            //    yield break;

            float time = 0;
            while (time < settings.DropSpeed)
            {
                time += Time.deltaTime;
                float perc = time / settings.DropSpeed;
                transform.position = Vector3.Lerp(startPos, endPos, perc);
                yield return null;
            }
            transform.position = endPos;
            Snap(SnapAxis.Both);
            tetro.EndFall();
        }
    }
}