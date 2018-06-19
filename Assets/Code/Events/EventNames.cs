using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stacker
{
    public static class EventNames
    {
        public const string InputReceived = "input-received";
        public const string StartGameUpdate = "start-update";
        public const string StopGameUpdate = "stop-update";
        public const string PauseGame = "pause-game";
        public const string ResumeGame = "resume-game";
        public const string EndGame = "end-game";
        public const string SetScore = "set-score";

        //public const string EarnCoin = "earn-coin";
        public const string AddCoins = "add-coins";
        public const string RemoveCoins = "remove-coins";
        public const string UpdateCoins = "update-coins";

        public const string PlaySFX = "play-sfx";

        public const string NewTetroFalling = "new-tetro-falling";
        public const string TetroEndFalling = "tetro-end-falling";
        public const string TetroOutOfBounds = "tetro-out-of-bounds";

        public const string GridFinishedUpdating = "grid-finished-updating";
        public const string RowCompleted = "row-completed";
        public const string SuccessfulDrop = "successful-drop";

        public const string TetroMoveRight = "tetro-move-right";
        public const string TetroMoveLeft = "tetro-move-left";
        public const string TetroMoveDrop = "tetro-move-drop";
        public const string TetroRotateClockwise = "tetro-rotate-clockwise";
        public const string TetroRotateCounterClockwise = "tetro-rotate-counter-clockwise";
    }
}