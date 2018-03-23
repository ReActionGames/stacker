using HenderStudios.Events;
using UnityEngine;

namespace Stacker
{
    public static class Currency
    {
        private static int numCoins = -1;

        public static int NumCoins
        {
            get
            {
                if (numCoins < 0)
                    LoadCoins();
                return numCoins;
            }
            set
            {
                numCoins = value;
                SaveCoins();
                EventManager.TriggerEvent(EventNames.UpdateCoins);
            }
        }

        public static void AddCoins(int amount)
        {
            NumCoins += amount;
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