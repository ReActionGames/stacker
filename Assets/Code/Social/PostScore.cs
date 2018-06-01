using HenderStudios.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stacker
{
    public class PostScore : MonoBehaviour
    {

        void Start()
        {
            EventManager.StartListening(EventNames.EndGame, OnEndGame);
        }

        private void OnEndGame(Message message)
        {
            int score = FindObjectOfType<Score>().score;
            if (Social.localUser.authenticated == false)
                return;
            Social.ReportScore(score, "CgkIkKf5rrQOEAIQAQ", (bool success) =>
            {
                Debug.Log("Reporting Score: " + (success ? "successful" : "unsuccessful"));
            });
        }
    }
}