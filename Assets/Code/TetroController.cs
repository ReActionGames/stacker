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
            InputTriggerType input = (InputTriggerType)msg.Data;
            ResolveInput(input);
        }

        private void ResolveInput(InputTriggerType input)
        {
            switch (input)
            {
                case InputTriggerType.TetroRight:
                    EventManager.TriggerEvent(EventNames.TetroMoveRight, new Message(tetroSettings));
                    break;
                case InputTriggerType.TetroLeft:
                    EventManager.TriggerEvent(EventNames.TetroMoveLeft, new Message(tetroSettings));
                    break;
                case InputTriggerType.TetroDrop:
                    EventManager.TriggerEvent(EventNames.TetroMoveDrop, new Message(tetroSettings));
                    break;
                case InputTriggerType.TetroClockwise:
                    EventManager.TriggerEvent(EventNames.TetroRotateClockwise, new Message(tetroSettings));
                    break;
                case InputTriggerType.TetroCounterClockwise:
                    EventManager.TriggerEvent(EventNames.TetroRotateCounterClockwise, new Message(tetroSettings));
                    break;
            }
        }
    }
}