using HenderStudios.Events;
using Stacker.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stacker
{
    public class KeyboardInput : MonoBehaviour, IInputHandler
    {
        [SerializeField] private KeyCode left = KeyCode.A;
        [SerializeField] private KeyCode leftAlt = KeyCode.LeftArrow;
        [SerializeField] private KeyCode right = KeyCode.D;
        [SerializeField] private KeyCode rightAlt = KeyCode.RightArrow;
        [SerializeField] private KeyCode drop = KeyCode.S;
        [SerializeField] private KeyCode dropAlt = KeyCode.DownArrow;
        [SerializeField] private KeyCode clockwise = KeyCode.Period;
        [SerializeField] private KeyCode counterClockwise = KeyCode.Comma;

        private void Update()
        {
            if (Input.anyKeyDown)
            {
                ResolveInput();
            }
        }

        public void ResolveInput()
        {
            InputTriggerType type = 0;
            if (Input.GetKeyDown(left) || Input.GetKeyDown(leftAlt))
                type = InputTriggerType.TetroLeft;
            else if (Input.GetKeyDown(right) || Input.GetKeyDown(rightAlt))
                type = InputTriggerType.TetroRight;
            else if (Input.GetKeyDown(drop) || Input.GetKeyDown(dropAlt))
                type = InputTriggerType.TetroDrop;
            else if (Input.GetKeyDown(clockwise))
                type = InputTriggerType.TetroClockwise;
            else if (Input.GetKeyDown(counterClockwise))
                type = InputTriggerType.TetroCounterClockwise;
            else
                return;

            SendTrigger(type);
        }

        private void SendTrigger(InputTriggerType type)
        {
            EventManager.TriggerEvent(EventNames.InputReceived, new Message(type));
        }
    }
}