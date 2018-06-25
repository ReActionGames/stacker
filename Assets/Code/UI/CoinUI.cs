using HenderStudios.Events;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Stacker
{
    public class CoinUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI coinText;

        private void Start()
        {
            UpdateUI();
            EventManager.StartListening(EventNames.UpdateCoins, UpdateUI);
        }

        private void UpdateUI()
        {
            coinText.text = Currency.NumCoins.ToString();
        }

        private void UpdateUI(Message message)
        {
            coinText.text = Currency.NumCoins.ToString();
        }
    }
}