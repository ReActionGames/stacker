using Stacker.ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stacker
{
    public class DifficultyIncreaser : MonoBehaviour
    {
        [SerializeField] private TetroSettings tetroSettings;
        
        void Start()
        {
            tetroSettings.ResetFallingSpeed();
        }

        private void FixedUpdate()
        {
            tetroSettings.IncreaseFallingSpeed();
        }
    }
}