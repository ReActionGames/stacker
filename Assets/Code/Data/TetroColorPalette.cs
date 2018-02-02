using Stacker.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stacker.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Data/Tetro Color Palette")]
    public class TetroColorPalette : ScriptableObject
    {

        [SerializeField]
        private Color I = Color.white;
        [SerializeField]
        private Color O = Color.white;
        [SerializeField]
        private Color T = Color.white;
        [SerializeField]
        private Color J = Color.white;
        [SerializeField]
        private Color L = Color.white;
        [SerializeField]
        private Color S = Color.white;
        [SerializeField]
        private Color Z = Color.white;

        public Color I_Color
        {
            get
            {
                return I;
            }
        }

        public Color O_Color
        {
            get
            {
                return O;
            }
        }

        public Color T_Color
        {
            get
            {
                return T;
            }
        }

        public Color J_Color
        {
            get
            {
                return J;
            }
        }

        public Color L_Color
        {
            get
            {
                return L;
            }

        }

        public Color S_Color
        {
            get
            {
                return S;
            }
        }

        public Color Z_Color
        {
            get
            {
                return Z;
            }
        }

        public Color GetColorBasedOnType(TetroType type)
        {
            switch (type)
            {
                case TetroType.I:
                    return I;
                case TetroType.O:
                    return O;
                case TetroType.T:
                    return T;
                case TetroType.J:
                    return J;
                case TetroType.L:
                    return L;
                case TetroType.S:
                    return S;
                case TetroType.Z:
                    return Z;
                default:
                    return O;
            }
        }
    }
}