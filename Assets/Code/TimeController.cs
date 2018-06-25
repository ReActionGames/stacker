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

            ResumeTime();
        }

        public void PauseTime()
        {
            previousDeltaTime = Time.fixedDeltaTime;
            previousTimeScale = Time.timeScale;

            Time.timeScale = 0;
            Time.fixedDeltaTime = 0;
        }

        public void ResumeTime()
        {
            //Time.timeScale = previousTimeScale;
            //Time.fixedDeltaTime = previousDeltaTime;
            Time.timeScale = 1;
            Time.fixedDeltaTime = 0.02f;
        }

        private void PauseTime(Message message)
        {
            PauseTime();
        }

        private void ResumeTime(Message message)
        {
            ResumeTime();
        }
    }
}