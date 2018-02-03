using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HenderStudios.Extensions;
using Stacker.Enums;
using Sirenix.OdinInspector;
using Stacker.Tetros;

namespace Stacker
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private Transform spawnPos;
        [SerializeField] private TetroPool tetroPool;
        [SerializeField] private float fallSpeed;

        private void Start()
        {
            PreWarmPool();
        }

        [Button]
        public void PreWarmPool()
        {
            tetroPool.PreWarm();
        }

        [Button]
        public void SpawnRandomTetro()
        {
            int length = System.Enum.GetNames(typeof(TetroType)).Length;
            int i = Random.Range(0, length) + 1;
            TetroType type = (TetroType)i;
            Tetro tetro = tetroPool.GetTetro(type);
            tetro.transform.position = spawnPos.position;
            tetro.gameObject.SetActive(true);
            tetro.StartFalling(fallSpeed);
        }
    }
}