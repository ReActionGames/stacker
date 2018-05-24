using HenderStudios.Events;
using Stacker.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stacker
{
    public class KeyboardInput : IInputHandler
    {
        [SerializeField] private KeyCode left = KeyCode.A;
        [SerializeField] private KeyCode leftAlt = KeyCode.LeftArrow;
        [SerializeField] private KeyCode right = KeyCode.D;
        [SerializeField] private KeyCode rightAlt = KeyCode.RightArrow;
        [SerializeField] private KeyCode drop = KeyCode.S;
        [SerializeField] private KeyCode dropAlt = KeyCode.DownArrow;
        [SerializeField] private KeyCode clockwise = KeyCode.Period;
        [SerializeField] private KeyCode counterClockwise = KeyCode.Comma;

        public bool IsInput()
        {
            return Input.anyKeyDown;
        }

        public InputTriggerType HandleInput()
        {
            InputTriggerType type = GetInputType();
            return type;
        }

        private InputTriggerType GetInputType()
        {
            if (Input.GetKeyDown(left) || Input.GetKeyDown(leftAlt))
                return InputTriggerType.TetroLeft;
            else if (Input.GetKeyDown(right) || Input.GetKeyDown(rightAlt))
                return InputTriggerType.TetroRight;
            else if (Input.GetKeyDown(drop) || Input.GetKeyDown(dropAlt))
                return InputTriggerType.TetroDrop;
            else if (Input.GetKeyDown(clockwise))
                return InputTriggerType.TetroClockwise;
            else if (Input.GetKeyDown(counterClockwise))
                return InputTriggerType.TetroCounterClockwise;
            else
                return 0;
        }

    }
}