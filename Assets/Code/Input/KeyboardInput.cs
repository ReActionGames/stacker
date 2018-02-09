using HenderStudios.Events;
using Stacker.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stacker
{
    public class KeyboardInput : MonoBehaviour
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

        private void ResolveInput()
        {
            InputType type = 0;
            if (Input.GetKeyDown(left) || Input.GetKeyDown(leftAlt))
                type = InputType.TetroLeft;
            else if (Input.GetKeyDown(right) || Input.GetKeyDown(rightAlt))
                type = InputType.TetroRight;
            else if (Input.GetKeyDown(drop) || Input.GetKeyDown(dropAlt))
                type = InputType.TetroDrop;
            else if (Input.GetKeyDown(clockwise))
                type = InputType.TetroClockwise;
            else if (Input.GetKeyDown(counterClockwise))
                type = InputType.TetroCounterClockwise;
            else
                return;

            SendTrigger(type);
        }

        private void SendTrigger(InputType type)
        {
            EventManager.TriggerEvent(EventNames.InputReceived, new Message(type));
        }
    }
}