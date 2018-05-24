using DoozyUI.Gestures;
using Stacker.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stacker
{
    public class TouchInput : IInputHandler
    {
        public bool IsInput()
        {
            return Input.touchCount > 0;
        }

        public InputTriggerType HandleInput()
        {
            throw new System.NotImplementedException();
        }

    }
}