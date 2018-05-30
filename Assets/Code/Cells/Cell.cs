using Sirenix.OdinInspector;
using Stacker.ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Stacker.Cells
{
    public class Cell : MonoBehaviour {

        [SerializeField] private CellState currentState;
        [SerializeField] private SpriteRenderer sprite;
        [Required]
        [SerializeField] private Image texture;
        [SerializeField] private TetroGrid grid;
        [SerializeField] private GameObject coinImage;
        
        public CellState CurrentState
        {
            get
            {
                return currentState;
            }
        }

        public int X
        {
            get; private set;
        }

        public int Y
        {
            get; private set;
        }

        public GameObject CoinImage
        {
            get
            {
                return coinImage;
            }
        }

        public SpriteRenderer Sprite
        {
            get
            {
                return sprite;
            }

            set
            {
                sprite = value;
            }
        }

        public TetroGrid Grid
        {
            get
            {
                return grid;
            }
            private set
            {
                grid = value;
            }
        }

        public Image Texture
        {
            get
            {
                return texture;
            }
        }

        private void Start()
        {
            currentState = new InactiveCell();
            currentState.OnEnterState(this);
        }

        public void ChangeState(CellState nextState)
        {
            currentState.OnExitState(this);
            currentState = nextState;
            currentState.OnEnterState(this);
        }

        public void SetGrid(TetroGrid grid, int x, int y)
        {
            this.Grid = grid;
            X = x;
            Y = y;
        }

        public void SetInactive()
        {
            ChangeState(new InactiveCell());
        }

        public Color GetColor()
        {
            return Sprite.color;
        }
    }
}