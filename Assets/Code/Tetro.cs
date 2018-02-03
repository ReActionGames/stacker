using HenderStudios.Events;
using Sirenix.OdinInspector;
using Stacker.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Stacker
{
    public class Tetro : MonoBehaviour
    {
        [SerializeField]
        private TetroType tetroType;

        [SerializeField] private TetroTile[] tiles;
        [SerializeField] private UnityEvent onStartFalling;
        [SerializeField] private UnityEvent onDie;

        private float fallSpeed;
        private bool falling = true;
        private TetroGrid grid;

        public UnityEvent OnStartFalling
        {
            get
            {
                return onStartFalling;
            }
        }
        public UnityEvent OnDie
        {
            get
            {
                return onDie;
            }
        }

        public TetroTile[] Tiles
        {
            get
            {
                return tiles;
            }
        }

        private void Start()
        {
            falling = false;
            grid = FindObjectOfType<TetroGrid>();
            ApplyColor();
            gameObject.SetActive(false);
        }

        public void StartFalling(float fallSpeed)
        {
            falling = true;
            this.fallSpeed = fallSpeed;
            gameObject.SetActive(true);
            EventManager.TriggerEvent(EventNames.NewTetroFalling, new Message(this));
            OnStartFalling?.Invoke();
            StartCoroutine(FallOneCell());
        }

        [Button]
        public void Restart()
        {
            gameObject.SetActive(true);
            AfterFall();
        }

        [Button]
        public void ApplyColor()
        {
            foreach (var tile in tiles)
            {
                tile.SetColor(tetroType);
            }
        }

        public TetroType GetTetroType()
        {
            return tetroType;
        }

        private IEnumerator FallOneCell()
        {
            Snap();

            falling = true;
            Vector3 startPos = transform.position;
            Vector2 nextCellPos = grid.GetCellPosBelow(transform.position);
            float time = 0;
            while (time < fallSpeed)
            {
                time += Time.deltaTime;
                float perc = time / fallSpeed;
                float yPos = Vector3.Lerp(startPos, nextCellPos, perc).y;
                transform.position = new Vector3(transform.position.x, yPos);
                yield return null;
            }
            Snap();
            falling = false;
            AfterFall();
        }

        private void AfterFall()
        {
            if (CanFall())
                StartCoroutine(FallOneCell());
            else
            {
                Snap();
                Die();
            }
        }

        private bool CanFall()
        {
            foreach (var tile in tiles)
            {
                if (grid.IsCellFull(grid.GetCellPosBelow(tile.transform.position)))
                    return false;
            }
            return true;
        }

        public void Snap()
        {
            FindObjectOfType<SnapToGrid>().Snap();
        }

        private void Die()
        {
            OnDie?.Invoke();
            foreach (var tile in tiles)
            {
                grid.SetCellFull(tile.transform.position, tetroType);
            }
            gameObject.SetActive(false);
        }
    }
}