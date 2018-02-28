using HenderStudios.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Stacker
{
    public class EndGameUI : MonoBehaviour {

        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private float speed;

        private void Start()
        {
            EventManager.StartListening(EventNames.SetScore, ShowUI);
        }

        private void ShowUI(Message message)
        {
            float score = (float)message.Data;
            scoreText.text = $"{score,0:00000}";

            StartCoroutine(FadeIn());
        }

        private IEnumerator FadeIn()
        {
            float time = 0;

            while (time < speed)
            {
                float perc = time / speed;
                SetAlpha(perc);
                time += Time.deltaTime;
                yield return null;
            }
        }

        private void SetAlpha(float percent)
        {
            canvasGroup.alpha = percent;
        }
    }
}