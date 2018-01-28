using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stacker
{
    public class TetroTile : MonoBehaviour
    {

        public void SetColor(Enums.TetroType type)
        {
            var grid = FindObjectOfType<TetroGrid>();
            GetComponent<SpriteRenderer>().color = grid.ColorPalette.GetColorBasedOnType(type);
        }

        private void OnDrawGizmos()
        {
            //Gizmos.DrawRay(transform.position + (Vector3.down * (size.y / 2)), Vector2.down * lookDist);
        }
    }
}