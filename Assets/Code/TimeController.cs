using HenderStudios.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stacker
{
    public class TimeController : MonoBehaviour
    {
        private float previousDeltaTime;
        private float previousTimeScale;

        private void Start()
        {
            EventManager.StartListening(EventNames.PauseGame, PauseTime);
            EventManager.StartListening(EventNames.ResumeGame, ResumeTime);
        }

        private void PauseTime(Message message)
        {
            previousDeltaTime = Time.fixedDeltaTime;
            previousTimeScale = Time.timeScale;

            Time.timeScale = 0;
            Time.fixedDeltaTime = 0;
        }

        private void ResumeTime(Message message)
        {
            Time.timeScale = previousTimeScale;
            Time.fixedDeltaTime = previousDeltaTime;
        }

    }
}