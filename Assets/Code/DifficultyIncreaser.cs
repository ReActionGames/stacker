using HenderStudios.Events;
using Stacker.ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stacker
{
    public class DifficultyIncreaser : MonoBehaviour
    {
        [SerializeField] private TetroSettings tetroSettings;

        private bool update = false;

        private void Start()
        {
            EventManager.StartListening(EventNames.StartGameUpdate, delegate
            {
                update = true;
            });
            EventManager.StartListening(EventNames.StopGameUpdate, delegate
            {
                update = false;
            });

            tetroSettings.ResetFallingSpeed();
        }

        private void FixedUpdate()
        {
            if (update == false)
                return;
            tetroSettings.IncreaseFallingSpeed();
        }
    }
}