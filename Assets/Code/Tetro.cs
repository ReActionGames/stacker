using Sirenix.OdinInspector;
using Stacker.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stacker
{
    public class Tetro : MonoBehaviour
    {
        [SerializeField]
        private Enums.TetroType tetroType;

        [SerializeField]
        private TetroTile[] tiles;

        private float fallSpeed;
        private bool falling = true;
        private TetroGrid grid;

        private void Start()
        {
            falling = false;
            grid = FindObjectOfType<TetroGrid>();
            ApplyColor();
        }

        public void StartFalling(float fallSpeed)
        {
            falling = true;
            this.fallSpeed = fallSpeed;
            gameObject.SetActive(true);
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
                transform.position = Vector3.Lerp(startPos, nextCellPos, perc);
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

        private void Snap()
        {
            FindObjectOfType<SnapToGrid>().Snap();
        }

        private void Die()
        {
            foreach (var tile in tiles)
            {
                grid.SetCellFull(tile.transform.position, tetroType);
            }
            gameObject.SetActive(false);
        }
    }
}