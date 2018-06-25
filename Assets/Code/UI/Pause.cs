using DoozyUI;
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
            Debug.Log("OnApplicationPause: " + pause);
            if (pause == true)
                return;

            Debug.Log("Sending PausGame Event...");
            EventManager.TriggerEvent(EventNames.PauseGame);
            UIManager.ShowUiElement("Pause", "Screens");
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
            HenderStudios.Extensions.SceneLoader.LoadScene("Main");
        }
    }
}