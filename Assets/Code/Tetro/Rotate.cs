using HenderStudios.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stacker.Tetros
{
    public class Rotate : MovementController
    {
        public override void SetUp(TetroMovement movement)
        {
            base.SetUp(movement);
            EventManager.StartListening(EventNames.TetroRotateClockwise, RotateClockwise);
            EventManager.StartListening(EventNames.TetroRotateCounterClockwise, RotateCounterClockwise);
        }

        private void RotateClockwise(Message msg)
        {
            throw new System.NotImplementedException();
        }

        private void RotateCounterClockwise(Message msg)
        {
            throw new System.NotImplementedException();
        }
    }
}