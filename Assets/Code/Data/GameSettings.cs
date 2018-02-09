using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stacker.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Data/Game Settings")]
    public class GameSettings : ScriptableObject
    {
        [SerializeField] private float tetroRespawnDelay;

        public float TetroRespawnDelay
        {
            get
            {
                return tetroRespawnDelay;
            }
        }
    }
}