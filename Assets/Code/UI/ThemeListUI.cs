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

        [Button]
        public void UpdateList()
        {
            if (Application.isPlaying == false)
                return;
            container.transform.Clear();

            foreach (Theme theme in themeData.Themes)
            {
                var go = Instantiate(themeUIPrefab);
                go.transform.SetParent(container.transform, false);
                go.SetUp(theme);
            }

            var rect = container.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(rect.sizeDelta.x, container.preferredHeight);
        }

    }
}