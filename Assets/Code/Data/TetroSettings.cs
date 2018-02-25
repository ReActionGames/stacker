using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;

namespace Stacker.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Data/Tetro Settings")]
    public class TetroSettings : ScriptableObject
    {
        [SerializeField] private float defaultFallingSpeed;
        [SerializeField] private float fallingSpeedIncreaseRate;
        [SerializeField] private float shiftSpeed;
        [SerializeField] private float dropSpeed;

        [ReadOnly]
        [SerializeField] private float fallingSpeed;

        public float DropSpeed
        {
            get
            {
                return dropSpeed;
            }
        }
        public float ShiftSpeed
        {
            get
            {
                return shiftSpeed;
            }
        }
        public float FallingSpeed
        {
            get
            {
                return fallingSpeed;
            }
        }

        public void ResetFallingSpeed()
        {
            fallingSpeed = defaultFallingSpeed;
        }
        public void IncreaseFallingSpeed()
        {
            fallingSpeed -= fallingSpeedIncreaseRate;
            fallingSpeed = Mathf.Clamp(fallingSpeed, 0.001f, defaultFallingSpeed);
        }
    }
}