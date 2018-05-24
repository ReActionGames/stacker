using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {

    private enum InputType
    {
        Auto,
        Keyboard,
        Touch
    }

    [SerializeField] private InputType inputType;
}
