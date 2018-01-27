using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceTile : MonoBehaviour {
    
    [SerializeField, TabGroup("Variables")]
    private float lookDist;

    [SerializeField, TabGroup("Variables")]
    private Vector2 size;
    [SerializeField, TabGroup("Variables")]
    private LayerMask floorMask;

    public bool CanMoveDown()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + (Vector3.down * (size.y / 2)), Vector2.down, lookDist, floorMask);
        return hit.collider == null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position + (Vector3.down * (size.y / 2)), Vector2.down * lookDist);
    }
}
