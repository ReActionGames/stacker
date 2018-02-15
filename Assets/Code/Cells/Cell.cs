using Stacker.ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stacker.Cells
{
    public class Cell : MonoBehaviour {

        [SerializeField]
        private CellState currentState;
        [SerializeField]
        internal SpriteRenderer sprite;
        [SerializeField]
        internal TetroGrid grid;
        
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
            this.grid = grid;
            X = x;
            Y = y;
        }

        public Color GetColor()
        {
            return sprite.color;
        }
    }
}