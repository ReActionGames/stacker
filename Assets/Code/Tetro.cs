using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stacker
{
    public class Tetro : MonoBehaviour
    {

        [SerializeField, Tooltip("The time it takes (in seconds) to move down from one cell to the next")]
        private float fallSpeed;
        [SerializeField]
        private Enums.TetroType tetroType;

        [SerializeField]
        private TetroTile[] tiles;

        private bool falling = true;
        private TetroGrid grid;

        private void Start()
        {
            //rb.velocity = Vector2.down * fallSpeed;
            falling = true;
            grid = FindObjectOfType<TetroGrid>();
            ApplyColor();
            //grid.OnGridUpdated.AddListener(AfterFall);
            StartCoroutine(FallOneCell());
        }

        [Button]
        public void Restart()
        {
            transform.position += Vector3.up * 5;
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

        private IEnumerator FallOneCell()
        {
            Snap();

            falling = true;
            Vector3 startPos = transform.position;
            Vector2 nextCellPos = grid.GetCellBelow(transform.position);
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
                if (grid.IsCellFull(grid.GetCellBelow(tile.transform.position)))
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