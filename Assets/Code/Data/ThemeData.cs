using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stacker.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Data/Theme List")]
    public class ThemeData : ScriptableObject
    {
        [SerializeField] private Theme[] themes;

        public Theme[] Themes
        {
            get
            {
                return themes;
            }
        }

        public Theme GetTheme(int themeNumber)
        {
            if (themeNumber < 0 || themeNumber >= themes.Length)
                return null;
            return themes[themeNumber];
        }
    }

    [Serializable]
    public class Theme
    {
        [SerializeField] private Sprite sprite;
        [SerializeField] private Color color;
        
        public Sprite Sprite
        {
            get
            {
                return sprite;
            }
        }

        public Color Color
        {
            get
            {
                return color;
            }
        }
    }
}