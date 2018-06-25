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
            return TouchInputDetector.GetTrigger() != 0;
        }

        public InputTriggerType HandleInput()
        {
            var temp = TouchInputDetector.GetTrigger();
            TouchInputDetector.ResetTrigger();
            return temp;
        }

    }
}