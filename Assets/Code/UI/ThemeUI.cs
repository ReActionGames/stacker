using DoozyUI;
using Sirenix.OdinInspector;
using Stacker.ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Stacker
{
    public class ThemeUI : MonoBehaviour
    {
        private static string unlockedKeyPrefix = "theme-unlocked-";
        private static string selectedKey = "theme-selected";
        private static int cost = 200;

        [SerializeField] private int themeNumber;
        [SerializeField] private Image image;
        [SerializeField] private RectTransform locked;
        [SerializeField] private RectTransform texture;
        [SerializeField] private RectTransform selected;

        private Theme theme;
        private string unlockedKey
        {
            get
            {
                return unlockedKeyPrefix + themeNumber;
            }
        }
        private bool unlocked;

        public void SetUp(ThemeData data)
        {
            theme = data.GetTheme(themeNumber);
            if (theme == null)
            {
                Destruct();
                return;
            }

            image.sprite = theme.Sprite;
            CheckIfUnlocked();
            CheckIfSelected();
        }

        public void OnClick()
        {
            if (unlocked)
            {
                Select();
                return;
            }

            AttemptPurchase();
        }

        private void AttemptPurchase()
        {
            bool successful = Currency.RemoveCoinsIfAble(cost);

            if (!successful)
                return;

            Unlock();
            Select();
        }

        private void CheckIfUnlocked()
        {
            unlocked = PlayerPrefsX.GetBool(unlockedKey, false);

            if (unlocked)
                Unlock();
        }

        private void CheckIfSelected()
        {
            int selected = PlayerPrefs.GetInt(selectedKey, 0);
            if (selected != themeNumber)
                return;
            Unlock();
            Select();
        }
        
        [Button]
        private void Select()
        {
            //Unlock();
            var listUI = GetComponentInParent<ThemeListUI>();
            listUI.DeselectAll();
            selected.gameObject.SetActive(true);

            listUI.SelectTheme(theme);
            PlayerPrefs.SetInt(selectedKey, themeNumber);
        }

        //[Button]
        public void Deselect()
        {
            selected.gameObject.SetActive(false);
        }

        [Button]
        private void Unlock()
        {
            PlayerPrefsX.SetBool(unlockedKey, true);
            unlocked = true;
            texture.gameObject.SetActive(true);
            locked.gameObject.SetActive(false);
        }

        private void Destruct()
        {
            //GetComponent<UIButton>().
            //Destroy(this.gameObject);
            gameObject.SetActive(false);
        }
    }
}