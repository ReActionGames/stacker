using HenderStudios.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stacker.Tetros
{
    public class Rotate : MovementController
    {
        private int rotationIndex;

        public override void SetUp(TetroMovement movement)
        {
            base.SetUp(movement);
            EventManager.StartListening(EventNames.TetroRotateClockwise, RotateClockwise);
            EventManager.StartListening(EventNames.TetroRotateCounterClockwise, RotateCounterClockwise);
        }

        private void RotateClockwise(Message msg)
        {
            if (tetro.Active == false)
                return;
            int temp = rotationIndex + 1;
            temp = tetro.Wrap(temp);
            if (CanRotate(temp) == false)
                return;
            rotationIndex = temp;
            tetro.SetRotation(rotationIndex);
        }

        private void RotateCounterClockwise(Message msg)
        {
            if (tetro.Active == false)
                return;
            int temp = rotationIndex - 1;
            temp = tetro.Wrap(temp);
            if (CanRotate(temp) == false)
                return;
            rotationIndex = temp;
            tetro.SetRotation(rotationIndex);
        }
    }
}