using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour {
        
    [SerializeField, Tooltip("The time it takes (in seconds) to move down from one cell to the next")]
    private float fallSpeed;

    [SerializeField]
    private PieceTile[] tiles;

    private bool falling = true;
    private TetrisGrid grid;

    private void Start()
    {
        //rb.velocity = Vector2.down * fallSpeed;
        falling = true;
        grid = FindObjectOfType<TetrisGrid>();
        //grid.OnGridUpdated.AddListener(AfterFall);
        StartCoroutine(FallOneCell());
    }
    
    [Button]
    public void Restart()
    {
        transform.position += Vector3.up * 5;
        AfterFall();
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
            if (grid.IsCellFull(tile.transform.position))
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
            grid.SetCellFull(tile.transform.position);
        }
        gameObject.SetActive(false);
    }
}
