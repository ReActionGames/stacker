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
            spawner.OnTetroDie.AddListener(OnTetroDie);
            StartCoroutine(WaitAndSpawnFirstTetro());
        }

        private IEnumerator WaitAndSpawnFirstTetro()
        {
            yield return new WaitForEndOfFrame();
            spawner.SpawnRandomTetro();
        }

        private void OnTetroDie()
        {
            bool fullRowsFound = grid.DeleteFullRows();

            float delay = grid.GameSettings.TetroRespawnDelayShort;
            if (fullRowsFound)
                delay = grid.GameSettings.TetroRespawnDelayLong;

            StartCoroutine(SpawnTetroAfterDelay(delay));
        }

        private IEnumerator SpawnTetroAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            spawner.SpawnRandomTetro();
        }
    }
}