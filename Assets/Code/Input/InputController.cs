using HenderStudios.Events;
using Stacker.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stacker
{
    public class InputController : MonoBehaviour
    {

        private enum InputType
        {
            Auto,
            Keyboard,
            Touch
        }

        [SerializeField] private InputType inputType;

        private IInputHandler inputHandler;

        private void Start()
        {
            inputHandler = EstablishInputController(inputType);
        }

        private void Update()
        {
            if (IsInput())
                HandleInput();
        }

        private IInputHandler EstablishInputController(InputType inputType)
        {
            switch (inputType)
            {
                case InputType.Keyboard:
                    return GetKeyboardInput();
                case InputType.Touch:
                    return GetTouchInput();
                case InputType.Auto:
                    return GetAutoInput();
                default:
                    return GetAutoInput();
            }
        }

        private bool IsInput()
        {
            return inputHandler.IsInput();
        }

        private void HandleInput()
        {
            var input = inputHandler.HandleInput();
            SendTrigger(input);
        }

        private void SendTrigger(InputTriggerType input)
        {
            Debug.Log("Sending Trigger: " + input.ToString());
            EventManager.TriggerEvent(EventNames.InputReceived, new Message(input));
        }


        private KeyboardInput GetKeyboardInput()
        {
            var input = new KeyboardInput();
            return input;
        }

        private TouchInput GetTouchInput()
        {
            var input = new TouchInput();
            return input;
        }

        private IInputHandler GetAutoInput()
        {
            if (Application.isMobilePlatform)
                return GetTouchInput();
            return GetKeyboardInput();
        }
    }
}