using HenderStudios.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stacker
{
    public class TetroMovement : MonoBehaviour
    {        
        private void Start()
        {
            EventManager.StartListening(EventNames.TetroMoveRight, ShiftRight);
            EventManager.StartListening(EventNames.TetroMoveLeft, ShiftLeft);
            EventManager.StartListening(EventNames.TetroMoveDrop, Drop);
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
    }
}