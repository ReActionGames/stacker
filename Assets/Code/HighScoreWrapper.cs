using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stacker
{
    public static class HighScoreWrapper
    {
        private const string highScoreKey = "high-score";

        private static int highScore = -1;

        public static int HighScore
        {
            get
            {
                if (highScore < 0)
                {
                    LoadHighScore();
                }
                return highScore;
            }

            private set
            {
                highScore = value;
            }
        }


        public static int LoadHighScore()
        {
            highScore = PlayerPrefs.GetInt(highScoreKey, 0);
            return highScore;
        }

        /// <summary>
        /// Tests to see if "score" is higher than the current high score. 
        /// If so, saves the new high score and returns true. 
        /// Otherwise, returns false.
        /// </summary>
        /// <param name="score"></param>
        /// <returns></returns>
        public static bool TestScore(int score)
        {
            if (score <= HighScore)
                return false;

            HighScore = score;
            SaveHighScore();
            return true;
        }

        public static void SaveHighScore()
        {
            PlayerPrefs.SetInt(highScoreKey, HighScore);

        }
    }
}