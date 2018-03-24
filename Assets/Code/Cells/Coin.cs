using HenderStudios.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stacker.Cells
{
    public class Coin : MonoBehaviour
    {
        [SerializeField] private AudioClip soundFx;

        public void PlaySoundFx()
        {
            EventManager.TriggerEvent(EventNames.PlaySFX, new Message(soundFx));
        }

        public void ReturnToPool()
        {
            gameObject.Recycle();
        }
    }
}