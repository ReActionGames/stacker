using Stacker.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Stacker.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Data/Tetro Data")]
    public class TetroData : SerializedScriptableObject
    {
        [SerializeField] private TetroType type;
        [SerializeField] private Vector2[] defaultTilePositions = new Vector2[4];
        //[LabelText("Tile Rotation Positions")]
        [SerializeField] private Vector2[,] tileRotationPositions = new Vector2[3,4];
    }
}