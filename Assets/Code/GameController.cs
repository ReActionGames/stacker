using HenderStudios.Events;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stacker
{
    public class GameController : MonoBehaviour
    {

        private Spawner spawner;
        private TetroGrid grid;

        private void Awake()
        {
            spawner = FindObjectOfType<Spawner>();
            grid = FindObjectOfType<TetroGrid>();
        }

        [Button]
        public void StartGame()
        {
            spawner.PreWarmPool();
            //spawner.OnTetroDie.AddListener(OnTetroDie);
            //EventManager.StartListening(EventNames.TetroEndFalling, OnTetroDie);
            EventManager.StartListening(EventNames.GridFinishedUpdating, OnGridFinishedUpdating);
            StartCoroutine(WaitAndSpawnFirstTetro());
        }

        private IEnumerator WaitAndSpawnFirstTetro()
        {
            yield return new WaitForEndOfFrame();
            spawner.SpawnRandomTetro();
        }

        //private void OnTetroDie(Message message)
        //{
        //    //grid.DeleteFullRows();
        //    //bool fullRowsFound = grid.DeleteFullRows();

        //    //float delay = grid.GameSettings.TetroRespawnDelayShort;
        //    //if (fullRowsFound)
        //    //    delay = grid.GameSettings.TetroRespawnDelayLong;

        //    //StartCoroutine(SpawnTetroAfterDelay(delay));
        //}

        private void OnGridFinishedUpdating(Message message)
        {
            float delay = (float)message.Data + grid.GameSettings.TetroRespawnDelay;
            StartCoroutine(SpawnTetroAfterDelay(delay));
            //SpawnTetroAfterDelay(delay);
        }

        private IEnumerator SpawnTetroAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            spawner.SpawnRandomTetro();
        }
    }
}