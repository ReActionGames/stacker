using HenderStudios.Events;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Stacker
{
    public class EndGameUI : MonoBehaviour {

        [Required]
        [SerializeField] private CanvasGroup canvasGroup;
        [Required]
        [SerializeField] private TextMeshProUGUI scoreText;
        [Required]
        [SerializeField] private TextMeshProUGUI highScoreText;
        [SerializeField] private float speed;

        private void Start()
        {
            EventManager.StartListening(EventNames.SetScore, ShowUI);
        }

        private void ShowUI(Message message)
        {
            int score = FindObjectOfType<Score>().score;
            scoreText.text = $"{score,0:00000}";
            HighScoreWrapper.TestScore(score);
            highScoreText.text = $"BEST {HighScoreWrapper.HighScore,0:00000}";

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
            SetAlpha(1);
        }

        private void SetAlpha(float percent)
        {
            canvasGroup.alpha = percent;
        }
    }
}