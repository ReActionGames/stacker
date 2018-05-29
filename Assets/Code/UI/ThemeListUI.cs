using HenderStudios.Extensions;
using Sirenix.OdinInspector;
using Stacker.ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Stacker
{
    public class ThemeListUI : MonoBehaviour
    {
        [SerializeField] private ThemeData themeData;
        [SerializeField] private ThemeUI themeUIPrefab;
        [SerializeField] private GridLayoutGroup container;

        private ThemeUI[] themeUIs;

        private void Start()
        {
            UpdateList();
        }

        //[Button]
        public void UpdateList()
        {
            themeUIs = GetComponentsInChildren<ThemeUI>();

            foreach (var theme in themeUIs)
            {
                theme.SetUp(themeData);
            }

            var rect = container.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(rect.sizeDelta.x, container.preferredHeight);
        }

        public void DeselectAll()
        {
            foreach (var theme in themeUIs)
            {
                theme.Deselect();
            }
        }

    }
}