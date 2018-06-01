using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stacker
{
    public class StatsUI : MonoBehaviour
    {

        public void OnStatsClicked()
        {
            Debug.Log("stats clicked.");
            Social.ShowLeaderboardUI();
            //GooglePlayGames.PlayGamesPlatform.Instance.ShowLeaderboardUI("CgkIkKf5rrQOEAIQAQ");
        }
    }
}