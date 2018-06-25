using HenderStudios.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stacker.Tetros
{
    public class Fall : MovementController
    {
        public override void SetUp(TetroMovement movement)
        {
            base.SetUp(movement);
            tetro.OnStartFalling.AddListener(OnStartFalling);
        }

        private void OnStartFalling()
        {
            StartCoroutine(FallDown());
        }

        private IEnumerator FallDown()
        {
            Snap(SnapAxis.Both);
            while (tetro.Active /*&& tetro.Falling*/ && CanFall())
            {
                yield return LerpFallOneCell();
                Snap(SnapAxis.Y);
            }
            EndFall();
        }

        private IEnumerator LerpFallOneCell()
        {
            Vector3 startPos = transform.position;
            Vector2 nextCellPos = grid.GetCellPosBelow(transform.position);
            float time = 0;
            while (tetro.Active /*&& tetro.Falling*/ && time < settings.FallingSpeed)
            {
                time += Time.deltaTime;
                float perc = time / settings.FallingSpeed;
                float yPos = Vector3.Lerp(startPos, nextCellPos, perc).y;
                transform.position = new Vector3(transform.position.x, yPos);
                yield return null;
            }
        }
        
        private void EndFall()
        {
            Snap(SnapAxis.Both);
            tetro.EndFall();
        }
    }
}