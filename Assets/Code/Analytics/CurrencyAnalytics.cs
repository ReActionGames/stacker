using HenderStudios.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stacker.Analytics
{
    public class CurrencyAnalytics : MonoBehaviour
    {

        private void Start()
        {
            EventManager.StartListening(EventNames.AddCoins, SendAddCoinsEvent);
        }

        private void SendAddCoinsEvent(Message msg)
        {
            UnityEngine.Analytics.AnalyticsEvent.Custom("AddCoins", new Dictionary<string, object>
            {
                {"coins", (int)msg.Data }
            });
        }
    }
}