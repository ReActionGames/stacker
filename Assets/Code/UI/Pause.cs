using HenderStudios.Events;
using HenderStudios.Extensions;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Stacker
{
    public class Pause : MonoBehaviour
    {

        private void OnApplicationPause(bool pause)
        {
            EventManager.TriggerEvent(EventNames.PauseGame);
        }

        public void PauseButtonClick()
        {
            EventManager.TriggerEvent(EventNames.PauseGame);
        }

        public void ResumeButtonClick()
        {
            EventManager.TriggerEvent(EventNames.ResumeGame);
        }

        public void HomeButtonClick()
        {
            EventManager.TriggerEvent(EventNames.ResumeGame);
            SceneLoader.LoadScene("Main");
        }
    }
}