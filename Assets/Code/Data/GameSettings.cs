using Sirenix.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stacker.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Data/Game Settings")]
    public class GameSettings : ScriptableObject
    {
        [SerializeField] private float tetroRespawnDelayShort;
        [SerializeField] private float tetroRespawnDelayLong;
        [SerializeField] private float tetroRespawnDelay;
        [SerializeField] private float rowFallDelay;
        [SerializeField] private float rowFallSpeed;
        [Range(0, 1)]
        [SerializeField] private float coinSpawnChance;

        public float TetroRespawnDelayShort
        {
            get
            {
                return tetroRespawnDelayShort;
            }
        }

        public float RowFallSpeed
        {
            get
            {
                return rowFallSpeed;
            }
            
        }

        public float RowFallDelay
        {
            get
            {
                return rowFallDelay;
            }
        }

        public float TetroRespawnDelayLong
        {
            get
            {
                return tetroRespawnDelayLong;
            }
        }

        public float TetroRespawnDelay
        {
            get
            {
                return tetroRespawnDelay;
            }
        }

        public float CoinSpawnChance
        {
            get
            {
                return coinSpawnChance;
            }
        }
    }
}