using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Stacker
{
    public class AudioToggle : MonoBehaviour
    {
        [SerializeField] private AudioMixer masterAudio;
        [SerializeField] private Image border;
        [SerializeField] private Image icon;
        [SerializeField] private ButtonState activeState;
        [SerializeField] private ButtonState inactiveState;

        [System.Serializable]
        private class ButtonState
        {
            [SerializeField] private Color borderColor;
            [SerializeField] private Color iconColor;

            public void ApplyState(Image border, Image icon)
            {
                border.color = borderColor;
                icon.color = iconColor;
            }
        }

        private bool audioOn = true;

        public void ToggleAudio()
        {
            audioOn = !audioOn;
            float volume = audioOn == true ? 0 : -80;
            masterAudio.SetFloat("MasterVolume", volume);

            ToggleState();
        }

        private void ToggleState()
        {
            if (audioOn)
                activeState.ApplyState(border, icon);
            else
                inactiveState.ApplyState(border, icon);
        }
    }
}