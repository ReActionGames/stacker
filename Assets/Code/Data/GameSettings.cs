using Sirenix.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stacker.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Data/Game Settings")]
    public class GameSettings : ScriptableObject
    {
        [SerializeField] private float tetroRespawnDelay;
        [SerializeField] private float rowFallDelay;
        [SerializeField] private float rowFallSpeed;

        public float TetroRespawnDelay
        {
            get
            {
                return tetroRespawnDelay;
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
    }
}