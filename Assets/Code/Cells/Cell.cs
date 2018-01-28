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

        public void SetGrid(TetroGrid grid)
        {
            this.grid = grid;
        }
    }
}