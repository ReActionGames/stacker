using HenderStudios.Events;
using HenderStudios.Extensions;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Stacker
{
    public class PauseUI : MonoBehaviour
    {
        [Required]
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private float speed;

        private void Start()
        {
            canvasGroup.blocksRaycasts = false;
            EventManager.StartListening(EventNames.PauseGame, ShowUI);
            EventManager.StartListening(EventNames.ResumeGame, HideUI);
        }

        private void ShowUI(Message message)
        {
            canvasGroup.blocksRaycasts = true;
            StartCoroutine(FadeIn());
        }

        private void HideUI(Message message)
        {
            canvasGroup.blocksRaycasts = false;
            StartCoroutine(FadeOut());
        }

        private IEnumerator FadeIn()
        {
            float time = 0;

            while (time < speed)
            {
                float perc = time / speed;
                SetAlpha(perc);
                time += Time.unscaledDeltaTime;
                yield return null;
            }
            SetAlpha(1);

            //EventManager.TriggerEvent(EventNames.PauseGame);
        }

        private IEnumerator FadeOut()
        {
            //EventManager.TriggerEvent(EventNames.ResumeGame);
            float time = 0;

            while (time < speed)
            {
                float perc = time / speed;
                SetAlpha(1 - perc);
                time += Time.unscaledDeltaTime;
                yield return null;
            }
            SetAlpha(0);
        }

        private void SetAlpha(float percent)
        {
            canvasGroup.alpha = percent;
        }

        public void PauseButtonClick()
        {
            //ShowUI();
            EventManager.TriggerEvent(EventNames.PauseGame);
        }

        public void ResumeButtonClick()
        {
            //HideUI();
            EventManager.TriggerEvent(EventNames.ResumeGame);
        }

        public void HomeButtonClick()
        {
            EventManager.TriggerEvent(EventNames.ResumeGame);
            SceneLoader.LoadScene("Main");
        }
    }
}