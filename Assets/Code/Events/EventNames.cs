using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stacker
{
    public static class EventNames
    {
        public const string InputReceived = "input-received";
        public const string NewTetroFalling = "new-tetro-falling";
        public const string TetroMoveRight = "tetro-move-right";
        public const string TetroMoveLeft = "tetro-move-left";
        public const string TetroMoveDrop = "tetro-move-drop";
        public const string TetroRotateClockwise = "tetro-rotate-clockwise";
        public const string TetroRotateCounterClockwise = "tetro-rotate-counter-clockwise";
    }
}