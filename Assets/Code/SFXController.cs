using HenderStudios.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stacker
{
    public class SFXController : MonoBehaviour
    {
        [SerializeField] private int maxSimultaneousSFX;

        private bool playFXFlag = false;
        private List<AudioClip> clipsToPlay = new List<AudioClip>();
        private AudioSource[] audioSources;
        private int currentSource = 0;
        
        private void Start()
        {
            AddAudioSources();
            EventManager.StartListening(EventNames.PlaySFX, AddFXToQueue);
        }

        private void AddAudioSources()
        {
            audioSources = new AudioSource[maxSimultaneousSFX];

            for (int i = 0; i < maxSimultaneousSFX; i++)
            {
                var source = gameObject.AddComponent<AudioSource>();
                audioSources[i] = source;
            }
        }

        private void AddFXToQueue(Message message)
        {
            AudioClip clip = (AudioClip)message.Data;
            if (clipsToPlay.Contains(clip))
                return;

            clipsToPlay.Add(clip);
            playFXFlag = true;
        }

        private void Update()
        {
            if (playFXFlag == false)
                return;

            PlayFXs();
        }

        private void PlayFXs()
        {
            foreach (AudioClip clip in clipsToPlay)
            {
                PlayClip(clip);
            }
            clipsToPlay.Clear();
            currentSource = 0;
            playFXFlag = false;
        }

        private void PlayClip(AudioClip clip)
        {
            currentSource++;
            if (currentSource >= maxSimultaneousSFX)
            {
                Debug.LogWarning("Cannot play SFX! Too many already playing!");
                return;
            }
            audioSources[currentSource].clip = clip;
            audioSources[currentSource].Play();
            Debug.Log("Playing Clip (" + clip.ToString() + ")");
        }

    }
}