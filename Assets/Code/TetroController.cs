using HenderStudios.Events;
using Stacker.Enums;
using Stacker.ScriptableObjects;
using UnityEngine;

namespace Stacker
{
    public class TetroController : MonoBehaviour
    {
        private TetroGrid grid;
        
        private TetroSettings tetroSettings
        {
            get
            {
                if (grid == null)
                    grid = FindObjectOfType<TetroGrid>();
                return grid.TetroSettings;
            }
        }

        private void Awake()
        {
            EventManager.StartListening(EventNames.InputReceived, OnInputReceived);
        }

        private void OnInputReceived(Message msg)
        {
            InputType input = (InputType)msg.Data;
            ResolveInput(input);
        }

        private void ResolveInput(InputType input)
        {
            switch (input)
            {
                case InputType.TetroRight:
                    EventManager.TriggerEvent(EventNames.TetroMoveRight, new Message(tetroSettings));
                    break;
                case InputType.TetroLeft:
                    EventManager.TriggerEvent(EventNames.TetroMoveLeft, new Message(tetroSettings));
                    break;
                case InputType.TetroDrop:
                    EventManager.TriggerEvent(EventNames.TetroMoveDrop, new Message(tetroSettings));
                    break;
                case InputType.TetroClockwise:
                    EventManager.TriggerEvent(EventNames.TetroRotateClockwise, new Message(tetroSettings));
                    break;
                case InputType.TetroCounterClockwise:
                    EventManager.TriggerEvent(EventNames.TetroRotateCounterClockwise, new Message(tetroSettings));
                    break;
            }
        }
    }
}