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
        [SerializeField] private float score;
        [Required]
        [SerializeField] private TextMeshProUGUI scoreText;
        [Required]
        [SerializeField] private RectTransform newHighScoreText;
        [SerializeField] private float scoreIncreaseRate;
        [SerializeField] private float rowBonus;

        private bool update = false;
        private bool beatHighScore = false;

        private void Start()
        {
            EventManager.StartListening(EventNames.StartGameUpdate, delegate
            {
                update = true;
            });
            EventManager.StartListening(EventNames.StopGameUpdate, StopUpdate);

            EventManager.StartListening(EventNames.RowCompleted, AddRowBonus);
        }

        private void StopUpdate(Message message)
        {
            update = false;
            EventManager.TriggerEvent(EventNames.SetScore, new Message(score));
        }

        private void AddRowBonus(Message message)
        {
            int numRows = (int)message.Data;
            score += rowBonus * numRows;
            UpdateScore();
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
            score += scoreIncreaseRate;
        }

        private void UpdateScore()
        {
            scoreText.text = $"{score,0:00000}";
            TestForHighScore();
        }

        private void TestForHighScore()
        {
            if (beatHighScore)
                return;

            beatHighScore = HighScoreWrapper.TestScore((int)score);
            if (beatHighScore)
                ActivateNewHighScore();
        }

        private void ActivateNewHighScore()
        {
            newHighScoreText.gameObject.SetActive(true);
        }
    }
}