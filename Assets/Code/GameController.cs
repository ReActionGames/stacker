using HenderStudios.Events;
using Sirenix.OdinInspector;
using Stacker.ScriptableObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Stacker
{
    public class GameController : MonoBehaviour
    {
        public bool startGameOnStart;
        [SerializeField] private GameSettings gameSettings;
        [SerializeField] private UnityEvent onGameOver;

        private Spawner spawner;
        private TetroGrid grid;

        public GameSettings GameSettings
        {
            get
            {
                return gameSettings;
            }
        }

        private void Awake()
        {
            spawner = FindObjectOfType<Spawner>();
            grid = FindObjectOfType<TetroGrid>();
        }

        private void Start()
        {
            if (startGameOnStart)
            {
                StartGame();
            }
        }

        [Button]
        public void StartGame()
        {
            spawner.PreWarmPool();
            //spawner.OnTetroDie.AddListener(OnTetroDie);
            //EventManager.StartListening(EventNames.TetroEndFalling, OnTetroDie);
            EventManager.StartListening(EventNames.GridFinishedUpdating, OnGridFinishedUpdating);
            EventManager.StartListening(EventNames.TetroOutOfBounds, EndGame);
            EventManager.TriggerEvent(EventNames.StartGameUpdate);
            StartCoroutine(WaitAndSpawnFirstTetro());
        }

        private void EndGame(Message message)
        {
            onGameOver?.Invoke();

            EventManager.TriggerEvent(EventNames.StopGameUpdate);
            EventManager.TriggerEvent(EventNames.EndGame);
        }

        private IEnumerator WaitAndSpawnFirstTetro()
        {
            yield return new WaitForEndOfFrame();
            spawner.SpawnRandomTetro();
        }
        
        private void OnGridFinishedUpdating(Message message)
        {
            float delay = (float)message.Data + grid.GameSettings.TetroRespawnDelay;
            StartCoroutine(SpawnTetroAfterDelay(delay));
        }

        private IEnumerator SpawnTetroAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            spawner.SpawnRandomTetro();
        }
    }
}