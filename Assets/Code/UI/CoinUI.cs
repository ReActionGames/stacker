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
            EventManager.StartListening(EventNames.AddCoins, UpdateUI);
            EventManager.StartListening(EventNames.RemoveCoins, UpdateUI);
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

    public static class Currency
    {
        public static int NumCoins
        {
            get; private set;
        }

        public static void AddCoins(int amount)
        {
            NumCoins += amount;
            SaveCoins();
            EventManager.TriggerEvent(EventNames.AddCoins, new Message(amount));
        }

        public static bool CanRemoveCoins(int amount)
        {
            return NumCoins >= amount;
        }

        public static bool RemoveCoinsIfAble(int amount)
        {
            if (!CanRemoveCoins(amount))
                return false;
            NumCoins -= amount;
            SaveCoins();
            EventManager.TriggerEvent(EventNames.RemoveCoins, new Message(amount));
            return true;
        }

        public static void SaveCoins()
        {
            PlayerPrefs.SetInt("coins", NumCoins);
        }

        public static void LoadCoins()
        {
            NumCoins = PlayerPrefs.GetInt("coins", 0);
        }
    }
}