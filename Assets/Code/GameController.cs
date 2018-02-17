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
            grid.DeleteFullRows();
            spawner.SpawnRandomTetro();
            //throw new NotImplementedException();
        }
    }
}