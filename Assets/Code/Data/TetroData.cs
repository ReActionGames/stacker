using Stacker.Enums;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Serialization;

namespace Stacker.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Data/Tetro Data")]
    public class TetroData : SerializedScriptableObject
    {
        [SerializeField] private TetroType type;
        [SerializeField] private int defaultTilePositionIndex = 0;
        //[LabelText("Tile Rotation Positions")]
        [FormerlySerializedAs("tileRotationPositions")]
        [SerializeField] private Vector2[,] tilePositions = new Vector2[3,4];

        public TetroType Type
        {
            get
            {
                return type;
            }
        }

        public Vector2[] DefaultTilePositions
        {
            get
            {
                Vector2[] temp = new Vector2[tilePositions.GetLength(1)];
                for (int i = 0; i < temp.Length; i++)
                {
                    temp[i] = tilePositions[defaultTilePositionIndex, i];
                }
                return temp;
            }
        }

        public Vector2[,] TilePositions
        {
            get
            {
                return tilePositions;
            }
        }

        public Vector2[] GetPositions(int index)
        {
            Vector2[] temp = new Vector2[tilePositions.GetLength(1)];
            for (int i = 0; i < temp.Length; i++)
            {
                temp[i] = tilePositions[index, i];
            }
            return temp;
        }
    }
}