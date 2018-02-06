using HenderStudios.Events;
using Stacker.ScriptableObjects;
using Stacker.Tetros;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stacker
{
    public class TetroMovement : MonoBehaviour
    {
        private Tetro tetro;
        private TetroGrid grid;
        private MovementController[] controllers;

        public Tetro Tetro
        {
            get
            {
                if (tetro == null)
                    tetro = GetComponent<Tetro>();
                return tetro;
            }
        }
        public TetroGrid Grid
        {
            get
            {
                if (grid == null)
                    grid = FindObjectOfType<TetroGrid>();
                return grid;
            }
        }
        public TetroSettings Settings
        {
            get
            {
                return Grid.TetroSettings;
            }
        }

        private enum SnapAxis
        {
            X,
            Y,
            Both
        }

        private void Start()
        {
            controllers = GetComponents<MovementController>();
            foreach (var c in controllers)
            {
                c.SetUp(this);
            }
        }

        public void Stop()
        {
            foreach (var c in controllers)
            {
                c.StopAllCoroutines();
            }
        }
    }
}