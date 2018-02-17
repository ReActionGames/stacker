using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stacker.Cells
{
    public class MovingCell : MonoBehaviour
    {
        private TetroGrid grid;

        private SpriteRenderer sprite
        {
            get
            {
                return GetComponent<SpriteRenderer>();
            }
        }
        
        public void MoveDown(TetroGrid grid, Cell cell, Vector2 startPos, int distance)
        {
            this.grid = grid;
            sprite.color = cell.GetColor();
            transform.position = startPos;

            Vector2 endPos = grid.GetCellPosAt(new Vector2(startPos.x, startPos.y - distance));
            StartCoroutine(LerpDown(startPos, endPos, ReturnToPool));
        }

        private IEnumerator LerpDown(Vector2 startPos, Vector2 endPos, System.Action callback)
        {
            float time = 0;
            float speed = grid.GameSettings.RowfallSpeed;
            while (time < speed)
            {
                time += Time.deltaTime;
                float perc = time / speed;
                float yPos = Vector3.Lerp(startPos, endPos, perc).y;
                transform.position = new Vector3(transform.position.x, yPos);
                yield return null;
            }
            callback?.Invoke();
        }

        private void ReturnToPool()
        {
            //grid.SetCellFull(transform.position);
            grid.SetCellFull(transform.position, GetColor());
            gameObject.Recycle();
        }

        private void SetColor(Color color)
        {
            sprite.color = color;
        }

        private Color GetColor()
        {
            return sprite.color;
        }
    }
}