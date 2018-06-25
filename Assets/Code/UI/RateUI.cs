using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stacker
{
    public class RateUI : MonoBehaviour
    {
        [SerializeField] private string url;

        public void OnRateClicked()
        {
            OpenURL();
        }

        private void OpenURL()
        {
            Application.OpenURL(url);
        }

    }
}