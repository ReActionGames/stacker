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

        private KeyboardInput GetKeyboardInput()
        {
            var input = gameObject.AddComponent<KeyboardInput>();
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