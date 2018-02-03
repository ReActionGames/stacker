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
            tetro.Falling = false;
            StartCoroutine(LerpDrop(grid.GetCellPosAt(transform.position + offset)));
        }

        private IEnumerator LerpDrop(Vector3 endPos)
        {
            Vector3 startPos =tetro.transform.position;

            if (grid.IsOutOfBounds(endPos))
                yield break;

            float time = 0;
            while (time < settings.DropSpeed)
            {
                time += Time.deltaTime;
                float perc = time / settings.ShiftSpeed;
                transform.position = Vector3.Lerp(startPos, endPos, perc);
                yield return null;
            }
            //transform.position = endPos;
            Snap(SnapAxis.Both);
            tetro.EndFall();
        }
    }
}