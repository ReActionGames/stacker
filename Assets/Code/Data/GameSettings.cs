using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stacker.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Data/Game Settings")]
    public class GameSettings : ScriptableObject
    {
        [SerializeField] private float tetroRespawnDelay;
        [SerializeField] private float rowfallSpeed;

        public float TetroRespawnDelay
        {
            get
            {
                return tetroRespawnDelay;
            }
        }

        public float RowfallSpeed
        {
            get
            {
                return rowfallSpeed;
            }
            
        }
    }
}