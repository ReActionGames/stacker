using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Tetro Settings")]
public class TetroSettings : ScriptableObject
{
    [SerializeField] private float fallingSpeed;
    [SerializeField] private float shiftSpeed;
    [SerializeField] private float dropSpeed;

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
}
