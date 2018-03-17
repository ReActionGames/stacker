using HenderStudios.Events;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Stacker
{
    public class Score : MonoBehaviour {

        [ReadOnly]
        [SerializeField] private float _score;
        [Required]
        [SerializeField] private TextMeshProUGUI scoreText;
        [Required]
        [SerializeField] private RectTransform newHighScoreText;
        [SerializeField] private float highScoreYOffset;
        [SerializeField] private float scoreIncreaseRate;
        [SerializeField] private float rowBonus;
        [SerializeField] private float dropBonus;

        private bool update = false;
        private bool beatHighScore = false;

        public int score
        {
            get
            {
                return (int)_score;
            }
        }

        private void Start()
        {
            EventManager.StartListening(EventNames.StartGameUpdate, delegate
            {
                update = true;
            });
            EventManager.StartListening(EventNames.StopGameUpdate, StopUpdate);

            EventManager.StartListening(EventNames.RowCompleted, AddRowBonus);
            EventManager.StartListening(EventNames.SuccessfulDrop, AddDropBonus);
        }

        private void StopUpdate(Message message)
        {
            update = false;
            EventManager.TriggerEvent(EventNames.SetScore, new Message(_score));
        }

        private void AddRowBonus(Message message)
        {
            int numRows = (int)message.Data;
            AddScore(rowBonus * numRows);
        }

        private void AddDropBonus(Message message)
        {
            float distanceDropped = (float)message.Data;
            AddScore(dropBonus * distanceDropped);
        }

        private void FixedUpdate()
        {
            if (update == false)
                return;
            IncrementScore();
            UpdateScore();
        }

        private void IncrementScore()
        {
            _score += scoreIncreaseRate;
        }

        private void AddScore(float amount)
        {
            _score += amount;
            UpdateScore();
        }

        private void UpdateScore()
        {
            scoreText.text = $"{_score,0:00000}";
            TestForHighScore();
        }

        private void TestForHighScore()
        {
            if (beatHighScore)
                return;

            beatHighScore = HighScoreWrapper.TestScore((int)_score);
            if (beatHighScore)
                ActivateNewHighScore();
        }

        private void ActivateNewHighScore()
        {
            newHighScoreText.gameObject.SetActive(true);
            RectTransform rect = scoreText.GetComponent<RectTransform>();
            rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, rect.anchoredPosition.y + highScoreYOffset);
        }
    }
}